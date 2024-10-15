import { HttpClientFactory } from "../../../../factories/HttpClientFactory";
import { IInvitationCheckDto } from "../../application/infrastructure/clients/InvitationsClient/IInvitationCheckDto";
import { IInvitationsClient } from "../../application/infrastructure/clients/InvitationsClient/IInvitationsClient";

export class InvitationsClient implements IInvitationsClient {

    private static readonly baseUrl = '/api/invitations';

    checkInvitationCode(invitationCode: string): Promise<IInvitationCheckDto> {
        const url = `${InvitationsClient.baseUrl}/check/${invitationCode}`;

        const httpClient = HttpClientFactory.createHttpClientWithAuth();

        return httpClient.post(url);
    }

}