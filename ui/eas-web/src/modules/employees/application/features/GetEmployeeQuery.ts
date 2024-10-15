import { IQuery } from "../../../../intermediary/IQuery";
import { FullName } from "../../../../sharedModels/FullName";
import { Guid } from "../../../../sharedModels/indexSharedModels";
import { Employee } from "../../domain/indexDomain";
import { EmployeesServiceCollection } from "../../EmployeesServiceCollection";
import { IEmployeesClient } from "../infrastructure/clients/EmployeesClient/IEmployeesClient";

export class GetEmployeeQuery implements IQuery<Employee> {

    private readonly employeesClient: IEmployeesClient;

    constructor(public readonly employeeId: Guid) {
        this.employeesClient = EmployeesServiceCollection.GetEmployeesClient();
    }

    execute(): Promise<Employee> {
        return this.employeesClient
            .getEmployee(this.employeeId)
            .then(empl => new Employee(empl.id,
                new FullName(empl.fullName.name, empl.fullName.surname, empl.fullName.patronymic),
                empl.birthday, empl.phoneNumber, empl.email, empl.state, empl.employmentDate)
            );
    }

}