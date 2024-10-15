import { NavigateFunction } from "react-router";
import { MessageBrokerConfig } from "../../../messageBroker/MessageBrokerConfig";
import { SelectedOrganizationChangedConsumer } from "./consumers/SelectedOrganizationChangedConsumer";

export class ConfigApiEmployees {

    public static configuration(navigation: NavigateFunction) {
        MessageBrokerConfig.RegistraionConsumer(new SelectedOrganizationChangedConsumer());
    }

}