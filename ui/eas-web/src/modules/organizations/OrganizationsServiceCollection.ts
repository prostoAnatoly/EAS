import { Mediator } from "../../intermediary/indexIntermediary";
import { IServiceBus } from "../../messageBroker/IServiceBus";
import { MemoryServiceBus } from "../../messageBroker/MemoryServiceBus";
import { IOrganizationsClient } from "./application/infrastructure/clients/OrganizationsClient/IOrganizationsClient";
import { OrganizationsClient } from "./infrastructure/clients/OrganizationsClient";

export class OrganizationsServiceCollection {

    public static getOrganizationsClient(): IOrganizationsClient {
        return new OrganizationsClient();
    }

    private static serviceBus: IServiceBus = new MemoryServiceBus();
    static getServiceBus(): IServiceBus {
        return OrganizationsServiceCollection.serviceBus;
    }

    private static readonly mediator: Mediator = new Mediator();
    public static GetMediator(): Mediator {
        return OrganizationsServiceCollection.mediator;
    }
}