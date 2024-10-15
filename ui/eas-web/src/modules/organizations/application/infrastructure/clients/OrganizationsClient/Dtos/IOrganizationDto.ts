import { Guid, ApiDate } from "../../../../../../../sharedModels/indexSharedModels";

export interface IOrganizationDto {
    id: Guid;
    name: string;
    createAt: ApiDate;
}