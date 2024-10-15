import { Guid } from "../../../../../../sharedModels/Guid";
import { CreateEmployeeArgs } from "./arguments/CreateEmployeeArgs";
import { GetEmployeesArgs } from "./arguments/GetEmployeesArgs";
import { UpdateEmployeeArgs } from "./arguments/UpdateEmployeeArgs";
import { ICreateEmployeeResponse } from "./responses/ICreateEmployeeResponse";
import { IGetEmployeeResponse } from "./responses/IGetEmployeeResponse";
import { IGetEmployeesResponse } from "./responses/IGetEmployeesResponse";
import { IUpdateEmployeeResponse } from "./responses/IUpdateEmployeeResponse";

export interface IEmployeesClient {

    getEmployees(args: GetEmployeesArgs): Promise<IGetEmployeesResponse>;

    createEmployee(args: CreateEmployeeArgs): Promise<ICreateEmployeeResponse>;

    updateEmployee(args: UpdateEmployeeArgs): Promise<IUpdateEmployeeResponse>;

    getEmployee(employeeId: Guid): Promise<IGetEmployeeResponse>;
}