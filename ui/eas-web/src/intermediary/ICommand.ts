export interface ICommandPromise{
    execute(): Promise<void>;
}

export interface ICommandPromiseWithResult<TResult> {
    execute(): Promise<TResult>;
}