import { IBusEvent } from "../../../../messageBroker/IBusEvent";
import { Guid } from "../../../../sharedModels/Guid";

export class SelectedOrganizationChangedIntegrationEvent implements IBusEvent {

    public static EVENT_NAME = 'SelectedOrganizationChangedIntegrationEvent';

    constructor(public readonly organizationId: Guid) { }

    eventName: string = SelectedOrganizationChangedIntegrationEvent.EVENT_NAME;
}