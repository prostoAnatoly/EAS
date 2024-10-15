import { IQuery } from "../../../../intermediary/indexIntermediary";
import { Organization } from "../../domain/Organization";
import { OrganizationsServiceCollection } from "../../OrganizationsServiceCollection";
import { IOrganizationsClient } from "../infrastructure/clients/OrganizationsClient/IOrganizationsClient";

export class GetOrganizationsQuery implements IQuery<Organization[]>{

    private readonly organizationsClient: IOrganizationsClient;

    constructor() {
        this.organizationsClient = OrganizationsServiceCollection.getOrganizationsClient();
    }

    execute(): Promise<Organization[]> {
        return this.organizationsClient.getOrganizations()
            .then(resp => resp.organizations
                .map(org =>
                    new Organization(org.id, org.name, org.createAt)));
    }

}