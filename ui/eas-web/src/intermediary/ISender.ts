import { ICommandPromise, ICommandPromiseWithResult } from "./indexIntermediary";

export interface ISender {

    send(request: ICommandPromise): Promise<void>;

    send<TResult>(request: ICommandPromiseWithResult<TResult>): Promise<TResult>;
}