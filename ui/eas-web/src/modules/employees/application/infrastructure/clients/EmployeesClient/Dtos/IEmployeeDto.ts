import { ApiDate, Guid } from "../../../../../../../sharedModels/indexSharedModels";
import { EmployeeState } from "../../../../../domain/indexDomain";
import { IFullNameDto } from "./IFullNameDto";

export interface IEmployeeDto {

    id: Guid;
    fullName: IFullNameDto;
    birthday: ApiDate;
    phoneNumber: string | undefined;
    email: string;
    state: EmployeeState;
    employmentDate: ApiDate;
}