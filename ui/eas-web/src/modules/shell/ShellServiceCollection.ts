import { IIdentityClient } from "./application/infrastructure/clients/IIdentityClient";
import { IdentityClient } from "./infrastructure/clients/IdentityClient";

export class ShellServiceCollection {

    static getIdentityClient(): IIdentityClient {
        return new IdentityClient();
    }

}