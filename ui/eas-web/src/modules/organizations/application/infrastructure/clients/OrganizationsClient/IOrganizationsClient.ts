import { CreateOrganizationArgs } from "./arguments/CreateOrganizationArgs";
import { ICreateOrganizationResponse } from "./responses/ICreateOrganizationResponse";
import { IGetOrganizationsResponse } from "./responses/IGetOrganizationsResponse";

export interface IOrganizationsClient {

    getOrganizations(): Promise<IGetOrganizationsResponse>;

    createOrganization(args: CreateOrganizationArgs): Promise<ICreateOrganizationResponse>;
}