import { RoutingBase } from "../../../config/RoutingBase";

export class RoutingByShell extends RoutingBase  {

    public static readonly ROOT = this.ROOT_APP + '/';
    public static readonly FORBIDDEN = this.ROOT_APP + '/forbidden';
    public static readonly NOT_FOUND = this.ROOT_APP + '/not-found';

    public static readonly LOGIN = this.ROOT_APP + '/login';

    public static readonly NO_CONNECTION_TO_SERVER = this.ROOT_APP + '/no_connect';
}