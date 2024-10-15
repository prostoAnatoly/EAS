import { NavigateFunction } from "react-router";
import { IConsumer } from "../../../../messageBroker/IConsumer";
import { ErrorRegisterIntegrationEvent } from "../../../register/contracts/integrationEvents";
import { RoutingByShell } from "../../config/routingByShell";
import { useNavigateSearchWithNav } from '../../../../utils/hooks';
import { QueryPath } from '../../../../utils/queryPath';

export class ErrorRegisterEventConsumer implements IConsumer<ErrorRegisterIntegrationEvent>
{
    private navigation: NavigateFunction;

    constructor(navigation: NavigateFunction) {
        this.navigation = navigation;
    }

    eventName: string = ErrorRegisterIntegrationEvent.EVENT_NAME;

    Consume(event: ErrorRegisterIntegrationEvent): Promise<void> {
        return new Promise(_ => {
            const navigateSearch = useNavigateSearchWithNav(this.navigation);

            navigateSearch(RoutingByShell.LOGIN, {
                [QueryPath.ERROR]: event.errorMessage,
            });
        });
    }
}