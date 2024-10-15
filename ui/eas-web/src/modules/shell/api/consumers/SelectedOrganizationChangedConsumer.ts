import { NavigateFunction } from "react-router";
import { IConsumer } from "../../../../messageBroker/IConsumer";
import { SelectedOrganizationChangedIntegrationEvent } from "../../../organizations/contracts/integrationEvents";
import { RoutingByShell } from "../../config/routingByShell";

export class SelectedOrganizationChangedConsumer implements IConsumer<SelectedOrganizationChangedIntegrationEvent>
{
    private navigation: NavigateFunction;

    constructor(navigation: NavigateFunction) {
        this.navigation = navigation;
    }

    eventName: string = SelectedOrganizationChangedIntegrationEvent.EVENT_NAME;

    Consume(event: SelectedOrganizationChangedIntegrationEvent): Promise<void> {
        return new Promise(_ => {
            this.navigation(RoutingByShell.ROOT);
        });
    }

}