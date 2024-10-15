import { NavigateFunction } from "react-router";
import { IConsumer } from "../../../../messageBroker/IConsumer";
import { RegisterLoginedIntegrationEvent } from "../../../register/contracts/integrationEvents";
import { RoutingByOrganizations } from "../../config/routingByOrganizations";

export class RegisterLoginedConsumer implements IConsumer<RegisterLoginedIntegrationEvent>
{
    private navigation: NavigateFunction;

    constructor(navigation: NavigateFunction) {
        this.navigation = navigation;
    }

    eventName: string = RegisterLoginedIntegrationEvent.EVENT_NAME;

    Consume(event: RegisterLoginedIntegrationEvent): Promise<void> {
        return new Promise(_ => {
            this.navigation(RoutingByOrganizations.ORGANIZATIONS);
        });
    }

}