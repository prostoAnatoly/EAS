import { ICommandPromise, ICommandPromiseWithResult } from "./ICommand";
import { IPipelineBehavior } from "./IPipelineBehavior";
import { ISender } from "./ISender";

export class Mediator implements ISender {

    private readonly pipelineBehaviors: IPipelineBehavior[] | undefined;

    constructor(pipelineBehaviors: IPipelineBehavior[] | undefined = undefined) {
        this.pipelineBehaviors = pipelineBehaviors;
    }

    send(request: ICommandPromise): Promise<void>
    send<TResult>(request: ICommandPromiseWithResult<TResult>): Promise<TResult>;
    send<TResult>(request: ICommandPromise | ICommandPromiseWithResult<TResult>): Promise<void> | Promise<TResult> {
        let handler = () => request.execute();

        if (this.pipelineBehaviors) {
            handler = this.pipelineBehaviors
                .reverse()
                .reduce((next, pipeline) => () => pipeline.handle(request, next), handler);
        }

        return handler();
    }

}