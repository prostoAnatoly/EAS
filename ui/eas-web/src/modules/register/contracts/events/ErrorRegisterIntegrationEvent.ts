import { IBusEvent } from "../../../../messageBroker/IBusEvent";

export class ErrorRegisterIntegrationEvent implements IBusEvent {
    static EVENT_NAME = 'ErrorRegisterIntegrationEvent';

    constructor(readonly errorMessage: string) { }

    eventName: string = ErrorRegisterIntegrationEvent.EVENT_NAME;

}