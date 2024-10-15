import { HttpClientFactory } from "../../../../factories/HttpClientFactory";
import { IUserProfileDto } from "../../application/infrastructure/clients/UsersClient/IUserProfileDto";
import { IUsersClient } from "../../application/infrastructure/clients/UsersClient/IUsersClient";

export class UsersClient implements IUsersClient {
    me(): Promise<IUserProfileDto> {
        const url = '/api/users/me';

        const httpClient = HttpClientFactory.createHttpClientWithAuth();

        return httpClient.get(url);
    }

}