import { ICommandPromise } from "../../../../intermediary/ICommand";
import { RegisterServiceCollection } from "../../RegisterServiceCollection";
import { IRegistrationClient } from "../infrastructure/clients/IRegistrationClient";

export class RegistrationUserByCodeCommand implements ICommandPromise {

    private readonly registrationClient: IRegistrationClient;

    constructor(public invitationCode: string, public userName: string, public password: string) {
        this.registrationClient = RegisterServiceCollection.getRegistrationClient();
    }

    execute() {
        return this.registrationClient.registrationUserByCode(this.invitationCode, this.userName, this.password);
    }
}