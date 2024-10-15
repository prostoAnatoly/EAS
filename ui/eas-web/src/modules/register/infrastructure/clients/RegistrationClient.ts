import { HttpClientFactory } from "../../../../factories/HttpClientFactory";
import { IRegistrationClient } from "../../application/infrastructure/clients/IRegistrationClient";

export class RegistrationClient implements IRegistrationClient {


    private static readonly baseUrl = '/api/registration';

    private static getUserModel(userName: string, password: string) {
        let passwordBase64 = window.btoa(password);
        const data = {
            userName: userName,
            password: passwordBase64,
        };

        return data;
    }

    registrationUser(userName: string, password: string): Promise<void> {
        const url = `${RegistrationClient.baseUrl}/user/`;

        const httpClient = HttpClientFactory.createHttpClientWithAuth();
        const data = RegistrationClient.getUserModel(userName, password);

        return httpClient.post(url, data);
    }

    registrationUserByCode(invitationCode: string, userName: string, password: string): Promise<void> {
        const url = `${RegistrationClient.baseUrl}/user-by-code/${invitationCode}`;

        const httpClient = HttpClientFactory.createHttpClientWithAuth();
        const data = RegistrationClient.getUserModel(userName, password);

        return httpClient.post(url, data);
    }
}