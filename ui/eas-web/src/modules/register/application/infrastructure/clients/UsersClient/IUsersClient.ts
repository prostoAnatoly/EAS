import { IUserProfileDto } from "./IUserProfileDto";

export interface IUsersClient {
    me(): Promise<IUserProfileDto>;
}