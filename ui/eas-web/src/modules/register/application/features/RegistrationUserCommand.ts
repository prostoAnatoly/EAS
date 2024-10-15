import { ICommandPromise } from "../../../../intermediary/ICommand";
import { RegisterServiceCollection } from "../../RegisterServiceCollection";
import { IRegistrationClient } from "../infrastructure/clients/IRegistrationClient";

export class RegistrationUserCommand implements ICommandPromise {

    private readonly registrationClient: IRegistrationClient;

    constructor(public userName: string, public password: string) {
        this.registrationClient = RegisterServiceCollection.getRegistrationClient();
    }

    execute() {
        return this.registrationClient.registrationUser(this.userName, this.password);
    }
}