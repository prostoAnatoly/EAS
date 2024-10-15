import { RoutingBase } from "../../../config/RoutingBase";

export class RoutingByEmployees extends RoutingBase  {

    public static readonly EMPLOYEES = this.ROOT_APP + '/employees';

    public static readonly CREATE_EMPLOYEE = this.ROOT_APP + '/employees/add';

    public static readonly EDIT_EMPLOYEE = this.ROOT_APP + '/employees/:id/edit';
}