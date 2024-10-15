import { IBusEvent } from "./IBusEvent";
import { IConsumer } from "./IConsumer";

export class MessageBrokerConfig {

    private static consumers: IConsumer<IBusEvent>[] = [];

    public static RegistraionConsumer(consumer: IConsumer<IBusEvent>) {
        MessageBrokerConfig.consumers.push(consumer);
    }

    public static getConsumers(): IConsumer<IBusEvent>[] {
        return MessageBrokerConfig.consumers;
    }
}