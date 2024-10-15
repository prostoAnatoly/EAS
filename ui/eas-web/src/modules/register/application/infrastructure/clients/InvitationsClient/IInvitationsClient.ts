import { IInvitationCheckDto } from "./IInvitationCheckDto";

export interface IInvitationsClient {
    checkInvitationCode(invitationCode: string): Promise<IInvitationCheckDto>
}