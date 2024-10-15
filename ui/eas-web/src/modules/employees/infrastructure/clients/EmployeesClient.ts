import { HttpClientFactory } from "../../../../factories/HttpClientFactory";
import { CreateEmployeeArgs } from "../../application/infrastructure/clients/EmployeesClient/arguments/CreateEmployeeArgs";
import { GetEmployeesArgs } from "../../application/infrastructure/clients/EmployeesClient/arguments/GetEmployeesArgs";
import { UpdateEmployeeArgs } from "../../application/infrastructure/clients/EmployeesClient/arguments/UpdateEmployeeArgs";
import { IEmployeesClient } from "../../application/infrastructure/clients/EmployeesClient/IEmployeesClient";
import { ICreateEmployeeResponse } from "../../application/infrastructure/clients/EmployeesClient/responses/ICreateEmployeeResponse";
import { IGetEmployeeResponse } from "../../application/infrastructure/clients/EmployeesClient/responses/IGetEmployeeResponse";
import { IGetEmployeesResponse } from "../../application/infrastructure/clients/EmployeesClient/responses/IGetEmployeesResponse";
import { IUpdateEmployeeResponse } from "../../application/infrastructure/clients/EmployeesClient/responses/IUpdateEmployeeResponse";
import { employeesStore } from "../../stores/EmployeesStore";

export class EmployeesClient implements IEmployeesClient {

    private getBaseUrl(): string {
        return `/api/employees/${employeesStore.selectedOrganizationId}`
    }

    getEmployees(args: GetEmployeesArgs): Promise<IGetEmployeesResponse> {
        const url = `${this.getBaseUrl()}/search`;

        const httpClient = HttpClientFactory.createHttpClientWithAuth();

        return httpClient.post<IGetEmployeesResponse>(url, args);
    }

    createEmployee(args: CreateEmployeeArgs): Promise<ICreateEmployeeResponse> {
        const url = this.getBaseUrl();

        const httpClient = HttpClientFactory.createHttpClientWithAuth();

        return httpClient.post<ICreateEmployeeResponse>(url, args);
    }

    updateEmployee(args: UpdateEmployeeArgs): Promise<IUpdateEmployeeResponse> {
        const url = `${this.getBaseUrl()}/${args.id}`;

        const httpClient = HttpClientFactory.createHttpClientWithAuth();

        return httpClient.put<IUpdateEmployeeResponse>(url, args);
    }

    getEmployee(employeeId: string): Promise<IGetEmployeeResponse> {
        const url = `${this.getBaseUrl()}/${employeeId}`;

        const httpClient = HttpClientFactory.createHttpClientWithAuth();

        return httpClient.get<IUpdateEmployeeResponse>(url);
    }
}