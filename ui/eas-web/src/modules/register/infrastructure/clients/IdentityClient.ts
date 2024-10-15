import { HttpClientFactory } from "../../../../factories/HttpClientFactory";
import { IAuthBaseInfoDto } from "../../application/infrastructure/clients/IdentityClient/IAuthBaseInfoDto";
import { IIdentityClient } from "../../application/infrastructure/clients/IdentityClient/IIdentityClient";

export class IdentityClient implements IIdentityClient {

    public login(userName: string, password: string): Promise<IAuthBaseInfoDto> {
        let passwordBase64 = window.btoa(password);
        const data = {
            userName: userName,
            password: passwordBase64,
        };
        const urlAuth = '/api/auth/login';

        const httpClient = HttpClientFactory.createHttpClient();

        return httpClient.post<IAuthBaseInfoDto>(urlAuth, data);
    }

}