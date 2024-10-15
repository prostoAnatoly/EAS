import { RoutingBase } from "../../../config/RoutingBase";

export class RoutingByRegister extends RoutingBase  {

    public static readonly REGISTRATION = this.ROOT_APP + '/registration';

    public static readonly USER_REGISTRATION = this.ROOT_APP + '/registration/user/new';

    /** Присоединение сотрудника к организации по приглашению */
    public static readonly JOIN_TO_ORGANIZATION = this.ROOT_APP + '/join-to-organization';

    public static readonly AGREEMENT_USER = this.ROOT_APP + '/user/agreement';
}