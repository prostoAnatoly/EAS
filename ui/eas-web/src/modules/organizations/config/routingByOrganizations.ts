import { RoutingBase } from "../../../config/RoutingBase";

export class RoutingByOrganizations extends RoutingBase  {

    public static readonly ORGANIZATIONS = this.ROOT_APP + '/organizations';

    public static readonly CREATE_ORGANIZATION = this.ROOT_APP + '/organizations/add';
}