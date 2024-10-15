import { IBusEvent } from "../../../../messageBroker/IBusEvent";

export class AlreadyRegisteredIntegrationEvent implements IBusEvent {
    static EVENT_NAME = 'AlreadyRegisteredIntegrationEvent';

    eventName: string = AlreadyRegisteredIntegrationEvent.EVENT_NAME;

}