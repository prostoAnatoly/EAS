import { ICommandPromiseWithResult } from "../../../../intermediary/indexIntermediary";
import { ApiDate, FullName, Guid } from "../../../../sharedModels/indexSharedModels";
import { Employee } from "../../domain/indexDomain";
import { EmployeesServiceCollection } from "../../EmployeesServiceCollection";
import { UpdateEmployeeArgs } from "../infrastructure/clients/EmployeesClient/arguments/UpdateEmployeeArgs";
import { IFullNameDto } from "../infrastructure/clients/EmployeesClient/Dtos/IFullNameDto";
import { IEmployeesClient } from "../infrastructure/clients/EmployeesClient/IEmployeesClient";

export class UpdateEmployeeCommand implements ICommandPromiseWithResult<Employee>{
    private readonly employeesClient: IEmployeesClient;

    constructor(
        public readonly id: Guid,
        public readonly fullName: FullName,
        public readonly birthday: ApiDate,
        public readonly phoneNumber: string | undefined,
        public readonly email: string,
        public readonly employmentDate: ApiDate) {

        this.employeesClient = EmployeesServiceCollection.GetEmployeesClient();
    }

    execute(): Promise<Employee> {
        const fullName: IFullNameDto = {
            name: this.fullName.name,
            surname: this.fullName.surname,
            patronymic: this.fullName.patronymic
        };
        const args = new UpdateEmployeeArgs(this.id, fullName,
            this.birthday, this.phoneNumber, this.email, this.employmentDate);

        return this.employeesClient
            .updateEmployee(args)
            .then(empl => {
                return new Employee(empl.id,
                    new FullName(empl.fullName.name, empl.fullName.surname, empl.fullName.patronymic),
                    empl.birthday, empl.phoneNumber, empl.email, empl.state, empl.employmentDate);
            });
    }

}