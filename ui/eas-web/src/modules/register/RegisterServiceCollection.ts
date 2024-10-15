import { Mediator } from "../../intermediary/indexIntermediary";
import { IServiceBus } from "../../messageBroker/IServiceBus";
import { MemoryServiceBus } from "../../messageBroker/MemoryServiceBus";
import { IIdentityClient } from "./application/infrastructure/clients/IdentityClient/IIdentityClient";
import { IInvitationsClient } from "./application/infrastructure/clients/InvitationsClient/IInvitationsClient";
import { IRegistrationClient } from "./application/infrastructure/clients/IRegistrationClient";
import { IUsersClient } from "./application/infrastructure/clients/UsersClient/IUsersClient";
import { TokenService } from "./application/infrastructure/services/TokenService";
import { IdentityClient } from "./infrastructure/clients/IdentityClient";
import { InvitationsClient } from "./infrastructure/clients/InvitationsClient";
import { RegistrationClient } from "./infrastructure/clients/RegistrationClient";
import { UsersClient } from "./infrastructure/clients/UsersClient";

export class RegisterServiceCollection {

    static getRegistrationClient(): IRegistrationClient {
        return new RegistrationClient();
    }

    static getIdentityClient(): IIdentityClient {
        return new IdentityClient();
    }

    static getTokenService(): TokenService {
        return new TokenService();
    }

    static getUsersClient(): IUsersClient {
        return new UsersClient();
    }

    static getInvitationsClient(): IInvitationsClient {
        return new InvitationsClient();
    }

    private static serviceBus: IServiceBus = new MemoryServiceBus();
    static getServiceBus(): IServiceBus {
        return RegisterServiceCollection.serviceBus;
    }

    private static readonly mediator: Mediator = new Mediator();
    public static GetMediator(): Mediator {
        return RegisterServiceCollection.mediator;
    }
}