import { IQuery } from "../../../../intermediary/IQuery";
import { Pagination } from "../../../../sharedModels/exTableView/Pagination";
import { FullName } from "../../../../sharedModels/FullName";
import { Employee } from "../../domain/indexDomain";
import { EmployeesServiceCollection } from "../../EmployeesServiceCollection";
import { GetEmployeesArgs } from "../infrastructure/clients/EmployeesClient/arguments/GetEmployeesArgs";
import { IEmployeesClient } from "../infrastructure/clients/EmployeesClient/IEmployeesClient";

export class GetEmployeesQuery implements IQuery<Pagination<Employee>> {

    private readonly employeesClient: IEmployeesClient;

    constructor(public readonly args: GetEmployeesArgs) {
        this.employeesClient = EmployeesServiceCollection.GetEmployeesClient();
    }

    execute(): Promise<Pagination<Employee>> {
        return this.employeesClient
            .getEmployees(this.args)
            .then(resp => new Pagination<Employee>(
                    resp.items.map(empl => new Employee(empl.id,
                        new FullName(empl.fullName.name, empl.fullName.surname, empl.fullName.patronymic),
                        empl.birthday, empl.phoneNumber, empl.email, empl.state, empl.employmentDate)),
                    resp.totalPages,
                    resp.total
            ));
    }

}