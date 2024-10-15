import { IConsumer } from "../../../../messageBroker/IConsumer";
import { SelectedOrganizationChangedIntegrationEvent } from "../../../organizations/contracts/integrationEvents";
import { employeesStore } from "../../stores/EmployeesStore";

export class SelectedOrganizationChangedConsumer implements IConsumer<SelectedOrganizationChangedIntegrationEvent>
{
    eventName: string = SelectedOrganizationChangedIntegrationEvent.EVENT_NAME;

    Consume(event: SelectedOrganizationChangedIntegrationEvent): Promise<void> {
        return new Promise(_ => {
            employeesStore.selectedOrganizationId = event.organizationId;
        });
    }

}