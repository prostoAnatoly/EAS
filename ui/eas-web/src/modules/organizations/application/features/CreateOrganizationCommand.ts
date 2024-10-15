import { ICommandPromiseWithResult } from "../../../../intermediary/indexIntermediary";
import { Organization } from "../../domain/indexDomain";
import { OrganizationsServiceCollection } from "../../OrganizationsServiceCollection";
import { CreateOrganizationArgs } from "../infrastructure/clients/OrganizationsClient/arguments/CreateOrganizationArgs";
import { IOrganizationsClient } from "../infrastructure/clients/OrganizationsClient/IOrganizationsClient";

export class CreateOrganizationCommand implements ICommandPromiseWithResult<Organization>{
    private readonly organizationsClient: IOrganizationsClient;

    constructor(public readonly name: string) {
        this.organizationsClient = OrganizationsServiceCollection.getOrganizationsClient();
    }

    execute(): Promise<Organization> {
        const args = new CreateOrganizationArgs(this.name);
        return this.organizationsClient
            .createOrganization(args)
            .then(resp => new Organization(resp.organization.id, resp.organization.name,
                resp.organization.createAt));
    }

}