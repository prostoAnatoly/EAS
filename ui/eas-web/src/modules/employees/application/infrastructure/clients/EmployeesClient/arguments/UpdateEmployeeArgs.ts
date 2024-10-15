import { ApiDate } from "../../../../../../../sharedModels/ApiDate";
import { Guid } from "../../../../../../../sharedModels/indexSharedModels";
import { IFullNameDto } from "../Dtos/IFullNameDto";

export class UpdateEmployeeArgs {
    constructor(
        public readonly id: Guid,
        public readonly fullName: IFullNameDto,
        public readonly birthday: ApiDate,
        public readonly phoneNumber: string | undefined,
        public readonly email: string,
        public readonly employmentDate: ApiDate) {

    }
}