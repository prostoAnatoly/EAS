export interface IIdentityClient  {

    logout(): Promise<boolean>;
}