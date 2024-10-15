import { Guid } from "../../../sharedModels/Guid";

export class OrganizationCard {
    constructor(public name: string, public inn: string, public organizationId: Guid) {

    }
}