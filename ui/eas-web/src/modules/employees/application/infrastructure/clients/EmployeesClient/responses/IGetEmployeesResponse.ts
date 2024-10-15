import { IEmployeeDto } from "../Dtos/IEmployeeDto";

export interface IGetEmployeesResponse {

    items: IEmployeeDto[];
    totalPages: number;
    total: number;
}