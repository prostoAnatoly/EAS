import { ICommandPromise } from "../../../../intermediary/ICommand";
import { FullName } from "../../../../sharedModels/FullName";
import { ProfileUser } from "../../domain/indexDomain";
import { RegisterServiceCollection } from "../../RegisterServiceCollection";
import { currentProfileUserStore } from "../../stores/CurrentProfileUserStore";
import { IIdentityClient } from "../infrastructure/clients/IdentityClient/IIdentityClient";
import { IUsersClient } from "../infrastructure/clients/UsersClient/IUsersClient";

export class LoginCommand implements ICommandPromise {

    private readonly identityClient: IIdentityClient;
    private readonly usersClient: IUsersClient;

    constructor(public userName: string, public password: string) {
        this.identityClient = RegisterServiceCollection.getIdentityClient();
        this.usersClient = RegisterServiceCollection.getUsersClient();
    }

    execute() {

        return this.identityClient.login(this.userName, this.password)
            .then(resp => {
                if (resp.accessToken) {
                    RegisterServiceCollection.getTokenService().create(resp.accessToken);

                    return Promise.resolve();
                }

                throw new Error('Вы не авторизованы');
            })
            .then(_ => this.usersClient.me()
                .then(p => {
                    currentProfileUserStore.Profile = new ProfileUser(
                        new FullName(p.fullName.name, p.fullName.surname, p.fullName.patronymic),
                        p.avatarUrl,
                        p.userName);
                })
                .catch(err => {
                    RegisterServiceCollection.getTokenService().clear();

                    throw err;
                })
            )
            .catch(err => {
                RegisterServiceCollection.getTokenService().clear();

                console.log(err);

                throw new Error('Что-то пошло не так. Код ошибки F-10');
            });
    }
}