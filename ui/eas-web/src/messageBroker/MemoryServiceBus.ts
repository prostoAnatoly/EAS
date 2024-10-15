import { IBusEvent } from "./IBusEvent";
import { IServiceBus } from "./IServiceBus";
import { MessageBrokerConfig } from "./MessageBrokerConfig";

export class MemoryServiceBus implements IServiceBus {

    publishEvent(event: IBusEvent): Promise<void> {
        return new Promise(_ => {
            const cunsumers = MessageBrokerConfig.getConsumers()
                .filter(x => x.eventName === event.eventName);

            if (cunsumers.length === 0) {
                console.log('Отсутствует потребитель события ' + event.eventName);

                throw new Error("Что-то пошло не так. Код ошибки F-50");
            }

            cunsumers.forEach(async x => await x.Consume(event));
        });
    }

}