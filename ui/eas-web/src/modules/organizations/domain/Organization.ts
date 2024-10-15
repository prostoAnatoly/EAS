import { ApiDate } from "../../../sharedModels/ApiDate";
import { Guid } from "../../../sharedModels/Guid";

export class Organization {
    constructor(public readonly id: Guid, public readonly name: string,
        public readonly createDate: ApiDate) {

    }
}