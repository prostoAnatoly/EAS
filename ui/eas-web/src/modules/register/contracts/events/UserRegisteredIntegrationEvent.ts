import { IBusEvent } from "../../../../messageBroker/IBusEvent";

export class UserRegisteredIntegrationEvent implements IBusEvent {
    static EVENT_NAME = 'UserRegisteredIntegrationEvent';

    eventName: string = UserRegisteredIntegrationEvent.EVENT_NAME;
}