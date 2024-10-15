import { ICommandPromiseWithResult } from "../../../../../intermediary/indexIntermediary";
import { RegisterServiceCollection } from "../../../RegisterServiceCollection";
import { IInvitationsClient } from "../../infrastructure/clients/InvitationsClient/IInvitationsClient";
import { InvitationCheckResult } from "./InvitationCheckResult";

export class CheckInvitationCodeCommand implements ICommandPromiseWithResult<InvitationCheckResult> {

    private readonly invitationsClient: IInvitationsClient;

    constructor(public invitationCode: string) {
        this.invitationsClient = RegisterServiceCollection.getInvitationsClient();
    }

    execute(): Promise<InvitationCheckResult> {
        return this.invitationsClient.checkInvitationCode(this.invitationCode)
            .then(resp => new InvitationCheckResult(resp.userName));
    }

}