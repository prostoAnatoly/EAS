import { IBusEvent } from "./IBusEvent";

export interface IConsumer<TBusEvent extends IBusEvent> {
    readonly eventName: string;
    Consume(event: TBusEvent): Promise<void>;
}