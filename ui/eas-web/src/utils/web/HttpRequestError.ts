
export class HttpRequestError extends Error {
    constructor(statusCode: number, message?: string) {
        super(message);
        this.statusCode = statusCode;
    }

    statusCode: number;

    public static createFromPromiseCatch(err: any): HttpRequestError {
        if (err instanceof HttpRequestError) {
            return err;
        }
        else {
            console.log(err);

            return new HttpRequestError(0, 'Что-то пошло не так. Код ошибки F-0');
        }
    }
}