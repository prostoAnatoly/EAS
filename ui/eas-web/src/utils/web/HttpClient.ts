import { api } from './../../config/api';
import { HttpRequestError } from './HttpRequestError';

interface IOkApiResponse<TPayload> {
    payload: TPayload;
}

interface IErrorApiResponse<TPayload> {
    payload: TPayload;
}

interface IResponseErrorBadRequestModel extends IResponseErrorBaseModel {
    errors?: { [key: string]: IErrorModel; };
}

interface IErrorModel {
    messages: string[];
}

interface ITooManyRequestsResponse {
    message?: string;
}

interface IResponseErrorBaseModel {
    messageBase?: string;
}

class ErrorHandlerDef {

    constructor(public message: string,
        public handler: (resp: Response, msg: string, hrefOfCalledPage?: string) => Promise<Response> | undefined) {
    }
}

export class HttpClient {
    private static readonly ERR_CONNECTION_REFUSED_TEXT = 'Отсутствует соединение с сервером';

    private static readonly POST: string = 'POST';
    private static readonly PUT: string = 'PUT';
    private static readonly GET: string = 'GET';
    private static readonly DELETE: string = 'DELETE';
    private static readonly MEMI_APP_JSON = 'application/json';

    private abortController: AbortController | null = null;
    private readonly locationWhereCalled: string;

    constructor(private accessToken: string | null = null,
        private isAllowCanceling: boolean = false, 
        public headers: Map<string, string> | null = null) {
        this.locationWhereCalled = location.href;
    }

    private static sharedErrorMessages: { [key: number]: ErrorHandlerDef } =
        HttpClient.getDefaultSharedErrorMessages();

    private static getDefaultSharedErrorMessages(): { [key: number]: ErrorHandlerDef } {
        const result: { [key: number]: ErrorHandlerDef } = {};

        return result;
    }

    private defaultErrorMessages: { [key: number]: ErrorHandlerDef } =
        this.getDefaultErrorMessages();

    private static createHttpRequestError(statusCode: number, message: string): HttpRequestError {
        return new HttpRequestError(statusCode, message);
    }

    private pushErrorMessage(pull: { [key: number]: ErrorHandlerDef }, statusCode: number, handler: ErrorHandlerDef): void {
        const val = pull[statusCode];
        if (val) {
            if (!val.message) {
                val.message = handler.message;
            }
            if (!val.handler) {
                val.handler = handler.handler;
            }
        }
        else {
            pull[statusCode] = handler;
        }
    }

    private getDefaultErrorMessages(): { [key: number]: ErrorHandlerDef } {
        const result = HttpClient.sharedErrorMessages;

        this.pushErrorMessage(result, 0, {
            message: HttpClient.ERR_CONNECTION_REFUSED_TEXT,
            handler: this.processEmptyError
        });

        this.pushErrorMessage(result, 400, {
            message: 'Данные не корректны',
            handler: (response, msgDefault) => {
                if (response.body) {
                    return (response.json() as Promise<IErrorApiResponse<IResponseErrorBadRequestModel>>)
                        .catch(() => { throw HttpClient.createHttpRequestError(response.status, msgDefault); })
                        .then(resp => {
                            if (resp && resp.payload) {
                                const err = resp.payload;
                                let message = resp.payload.messageBase || '';
                                if (err.errors) {
                                    for (const [key, value] of Object.entries(err.errors)) {
                                        if (value.messages) {
                                            if (message) {
                                                message += '. ';
                                            }
                                            message += value.messages.join(' ');
                                        }
                                    }
                                }
                                throw HttpClient.createHttpRequestError(response.status, message ? message : msgDefault);
                            }
                            throw HttpClient.getUnexpectedResponseFromServer();
                        });
                }
                throw HttpClient.createHttpRequestError(response.status, msgDefault);
            }
        });

        this.pushErrorMessage(result, 401, {
            message: 'Вы не авторизованы',
            handler: this.processEmptyError
        });

        this.pushErrorMessage(result, 403, {
            message: 'Доступ запрещён',
            handler: this.processEmptyError
        });

        this.pushErrorMessage(result, 404, {
            message: 'Запрашиваемый ресурс не найден',
            handler: this.processStandardError
        });

        this.pushErrorMessage(result, 405, {
            message: 'Метод не поддерживается',
            handler: this.processEmptyError
        });

        this.pushErrorMessage(result, 409, {
            message: 'Отсутствует соединение с сервером',
            handler: this.processStandardError
        });

        this.pushErrorMessage(result, 429, {
            message: 'Достигнут предел кол-ва попыток',
            handler: (response, msgDefault) => {
                if (response.body) {
                    return (response.json() as Promise<IErrorApiResponse<ITooManyRequestsResponse>>)
                        .catch(() => { throw HttpClient.createHttpRequestError(response.status, msgDefault); })
                        .then(resp => {
                            if (resp && resp.payload) {
                                throw HttpClient.createHttpRequestError(response.status, msgDefault);
                            }
                            throw HttpClient.getUnexpectedResponseFromServer();
                        });
                }
                throw HttpClient.createHttpRequestError(response.status, msgDefault);
            }
        });

        this.pushErrorMessage(result, 500, {
            message: 'Внутренняя ошибка сервера',
            handler: this.processStandardError
        });

        this.pushErrorMessage(result, 503, {
            message: 'Сервер недоступен',
            handler: this.processEmptyError
        });

        this.pushErrorMessage(result, 504, {
            message: 'Таймаут',
            handler: this.processEmptyError
        });

        return result;
    }

    private processStandardError(response: Response, msgDefault: string): Promise<Response> {
        if (response.body) {
            return (response.json() as Promise<IErrorApiResponse<IResponseErrorBaseModel>>)
                .catch(() => { throw HttpClient.createHttpRequestError(response.status, msgDefault); })
                .then(resp => {
                    if (resp && resp.payload) {
                        const err = resp.payload;
                        const message = err.messageBase || '';
                        throw HttpClient.createHttpRequestError(response.status, message ? message : msgDefault);
                    }
                    throw HttpClient.getUnexpectedResponseFromServer();
                });
        }
        throw HttpClient.createHttpRequestError(response.status, msgDefault);
    }

    private processEmptyError(response: Response, msgDefault: string): Promise<Response> {
        throw HttpClient.createHttpRequestError(response.status, msgDefault);
    }

    private static getUnexpectedResponseFromServer(): HttpRequestError {
        return HttpClient.createHttpRequestError(0, 'Непредвиденный ответ от сервера');
    }

    private processHandler<TOkPayload>(response: Response): Promise<TOkPayload> {
        if (!response.ok) { // Обработка ошибок
            const status = response.status;
            const handlerDef = this.defaultErrorMessages[status];
            if (handlerDef !== undefined) {
                let h = handlerDef.handler(response, handlerDef.message, this.locationWhereCalled);
                if (!h) {
                    h = this.processStandardError(response, handlerDef.message);
                }
                return h.then(() => undefined as unknown as TOkPayload);
            }
            throw HttpClient.createHttpRequestError(response.status, 'Непредвиденная ошибка сервера');
        }

        return (response.json() as Promise<IOkApiResponse<TOkPayload>>)
            .then(ok => {
                if (ok && ok.payload) {
                    return ok.payload;
                }
                throw HttpClient.getUnexpectedResponseFromServer();
            });
    }

    private getFullUrl(url: string): string {
        let baseUrl = api.rootApiUrl;
        if (!baseUrl) {
            baseUrl = '';
        }
        return url = url.startsWith('/') ? baseUrl + url : url;
    }

    private send<TOkPayload>(input: RequestInfo, method: string, data?: any): Promise<TOkPayload> {
        const headers: Headers = new Headers();
        headers.append('Accept', HttpClient.MEMI_APP_JSON);

        // Добавить Content-Type
        if (method === HttpClient.POST || method === HttpClient.PUT) {
            headers.append('Content-Type', HttpClient.MEMI_APP_JSON);
        }

        // Добавить Authorization
        if (this.accessToken) {
            headers.append('Authorization', `Bearer ${this.accessToken}`);
        }

        if (this.headers) {
            this.headers.forEach((v, k) => headers.append(k, v))
        }

        const init: RequestInit = {
            method: method,
            body: data ? JSON.stringify(data) : null,
            headers: headers
        }

        // установить абсолютный url, если требуется.
        if (input instanceof Request) {
            const req = (input as Request);
            input = new Request(this.getFullUrl(req.url), req);
        }
        else {
            const url: string = input as string;
            input = this.getFullUrl(url);
        }

        // Установить сигнал отмены, если включена данная опция
        if (this.isAllowCanceling) {
            this.abortController = new AbortController();
            init.signal = this.abortController.signal;
        }

        // отправить реальный запрос
        return fetch(input, init).catch(async () => { // Сбой сети или что-то помешало связаться с сервером
            if (HttpClient.handlerErrConnectionRefuused) {
                await HttpClient.handlerErrConnectionRefuused();

                return undefined;
            }
            throw HttpClient.createHttpRequestError(0, HttpClient.ERR_CONNECTION_REFUSED_TEXT);
        }).then(response => {
            if (response) {
                return this.processHandler(response);
            }

            return undefined as unknown as TOkPayload;
        });
    }

    private static handlerErrConnectionRefuused: () => Promise<void> | undefined;
    public static setHandlerErrConnectionRefuused(handler: () => Promise<void> | undefined) {
        HttpClient.handlerErrConnectionRefuused = handler;
    }

    public static addResponseHandlerByStatusCode(statusCode: number,
        handler: (resp: Response, msgDefault: string, locationWhereCalled?: string) => Promise<Response> | undefined): void {

        const handlerDef = HttpClient.sharedErrorMessages[statusCode];
        if (handlerDef !== undefined) {
            handlerDef.handler = handler;
        }
        else {
            HttpClient.sharedErrorMessages[statusCode] = { message: '', handler: handler, };
        }
    }

    public post<TOkPayload>(input: RequestInfo, data?: any): Promise<TOkPayload> {
        return this.send<TOkPayload>(input, HttpClient.POST, data);
    }

    public put<TOkPayload>(input: RequestInfo, data: any): Promise<TOkPayload> {
        return this.send<TOkPayload>(input, HttpClient.PUT, data);
    }

    public delete<TOkPayload>(input: RequestInfo): Promise<TOkPayload> {
        return this.send<TOkPayload>(input, HttpClient.DELETE);
    }

    public get<TOkPayload>(input: RequestInfo): Promise<TOkPayload> {
        return this.send<TOkPayload>(input, HttpClient.GET);
    }

    public cancel(): void {
        if (this.isAllowCanceling && this.abortController) {
            this.abortController.abort();
        }
    }
}