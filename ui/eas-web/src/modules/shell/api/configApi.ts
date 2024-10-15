import { NavigateFunction } from "react-router";
import { MessageBrokerConfig } from "../../../messageBroker/MessageBrokerConfig";
import { AlreadyRegisteredConsumer } from "./consumers/AlreadyRegisteredConsumer";
import { ErrorRegisterEventConsumer } from "./consumers/ErrorRegisterEventConsumer";
import { SelectedOrganizationChangedConsumer } from "./consumers/SelectedOrganizationChangedConsumer";

export class ConfigApiShell {

    public static configuration(navigation: NavigateFunction) {
        MessageBrokerConfig.RegistraionConsumer(new ErrorRegisterEventConsumer(navigation));
        MessageBrokerConfig.RegistraionConsumer(new AlreadyRegisteredConsumer(navigation));
        MessageBrokerConfig.RegistraionConsumer(new SelectedOrganizationChangedConsumer(navigation));
    }
}