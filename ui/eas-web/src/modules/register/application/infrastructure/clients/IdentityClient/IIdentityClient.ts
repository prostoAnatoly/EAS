import { IAuthBaseInfoDto } from "./IAuthBaseInfoDto";

export interface IIdentityClient  {

    login(userName: string, password: string): Promise<IAuthBaseInfoDto>;
}