export interface IRegistrationClient {
    registrationUserByCode(invitationCode: string, userName: string, password: string): Promise<void>;

    registrationUser(userName: string, password: string): Promise<void>;
}