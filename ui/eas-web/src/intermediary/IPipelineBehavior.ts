import { ICommandPromise, ICommandPromiseWithResult } from "./indexIntermediary";


export interface IPipelineBehavior {

    handle<TResult>(
        command: ICommandPromise | ICommandPromiseWithResult<TResult>,
        next: () => Promise<TResult> | Promise<void>): Promise<TResult> | Promise<void>;
}