import { ICommandPromiseWithResult } from "./ICommand";

export interface IQuery<TResult> extends ICommandPromiseWithResult<TResult> {
    execute(): Promise<TResult>;
}