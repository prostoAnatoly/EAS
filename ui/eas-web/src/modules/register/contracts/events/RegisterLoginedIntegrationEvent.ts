import { IBusEvent } from "../../../../messageBroker/IBusEvent";

export class RegisterLoginedIntegrationEvent implements IBusEvent {
    static EVENT_NAME = 'RegisterLoginedIntegrationEvent';

    eventName: string = RegisterLoginedIntegrationEvent.EVENT_NAME;
}