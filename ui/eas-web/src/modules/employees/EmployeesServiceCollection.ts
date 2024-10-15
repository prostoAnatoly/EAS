import { Mediator } from "../../intermediary/indexIntermediary";
import { IEmployeesClient } from "./application/infrastructure/clients/EmployeesClient/IEmployeesClient";
import { EmployeesClient } from "./infrastructure/clients/EmployeesClient";

export class EmployeesServiceCollection {

    public static GetEmployeesClient(): IEmployeesClient
    {
        return new EmployeesClient();
    }

    private static readonly mediator: Mediator = new Mediator();
    public static GetMediator(): Mediator {
        return EmployeesServiceCollection.mediator;
    }
}