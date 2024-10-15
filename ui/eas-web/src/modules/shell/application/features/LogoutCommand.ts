import { ICommandPromise } from "../../../../intermediary/indexIntermediary";
import { Token } from "../../../../utils/Token";
import { ShellServiceCollection } from "../../ShellServiceCollection";
import { IIdentityClient } from "../infrastructure/clients/IIdentityClient";

export class LogoutCommand implements ICommandPromise {

    private readonly identityClient: IIdentityClient;

    constructor() {
        this.identityClient = ShellServiceCollection.getIdentityClient();
    }

    execute() {
        return this.identityClient.logout()
            .then(resp => {
                if (resp) {
                    Token.clear();
                }
            }).catch(() => {
                Token.clear();
            });
    }
}