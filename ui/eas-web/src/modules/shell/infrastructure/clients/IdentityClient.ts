import { HttpClientFactory } from "../../../../factories/HttpClientFactory";
import { IIdentityClient } from "../../application/infrastructure/clients/IIdentityClient";

export class IdentityClient implements IIdentityClient {

    public logout(): Promise<boolean> {
        const urlLogout = '/api/auth/logout';
        const httpClient = HttpClientFactory.createHttpClientWithAuth();

        return httpClient.get<boolean>(urlLogout);
    }

}