import { NavigateFunction } from "react-router";
import { MessageBrokerConfig } from "../../../messageBroker/MessageBrokerConfig";
import { RegisterLoginedConsumer } from "./consumers/RegisterLoginedConsumer";

export class ConfigApiOrganizations {

    public static configuration(navigation: NavigateFunction) {
        MessageBrokerConfig.RegistraionConsumer(new RegisterLoginedConsumer(navigation));
    }
}