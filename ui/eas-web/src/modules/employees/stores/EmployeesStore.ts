import { makeAutoObservable } from "mobx";
import { Guid } from "../../../sharedModels/indexSharedModels";

class EmployeesStore {

    public selectedOrganizationId: Guid | undefined = undefined;

    constructor() {
        makeAutoObservable(this);
    }
}

export const employeesStore = new EmployeesStore();