import { ApiDate } from "../../../../../../../sharedModels/ApiDate";
import { IFullNameDto } from "../Dtos/IFullNameDto";

export class CreateEmployeeArgs {
    constructor(public readonly fullName: IFullNameDto,
        public readonly birthday: ApiDate,
        public readonly phoneNumber: string | undefined,
        public readonly email: string,
        public readonly employmentDate: ApiDate) {

    }
}