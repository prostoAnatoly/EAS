import { NavigateFunction } from "react-router";
import { IConsumer } from "../../../../messageBroker/IConsumer";
import { AlreadyRegisteredIntegrationEvent } from "../../../register/contracts/integrationEvents";
import { RoutingByShell } from "../../config/routingByShell";

export class AlreadyRegisteredConsumer implements IConsumer<AlreadyRegisteredIntegrationEvent>
{
    private navigation: NavigateFunction;

    constructor(navigation: NavigateFunction) {
        this.navigation = navigation;
    }

    eventName: string = AlreadyRegisteredIntegrationEvent.EVENT_NAME;

    Consume(event: AlreadyRegisteredIntegrationEvent): Promise<void> {
        return new Promise(_ => {
            this.navigation(RoutingByShell.LOGIN);
        });
    }

}