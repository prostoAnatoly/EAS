import { ApiDate, FullName, Guid } from "../../../../sharedModels/indexSharedModels";
import { EmployeeState } from "../indexDomain";

export class Employee {

    constructor(public readonly id: Guid, public readonly fullName: FullName, public readonly birthday: ApiDate,
        public readonly phoneNumber: string | undefined, public readonly email: string,
        public readonly state: EmployeeState, public readonly employmentDate: ApiDate) {

    }

}