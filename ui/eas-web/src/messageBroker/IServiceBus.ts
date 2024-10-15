import { IBusEvent } from "./IBusEvent";

export interface IServiceBus {
    publishEvent(event: IBusEvent): Promise<void>;
}