import { MenuItemDef } from "../../../kit/kit";
import { RoutingByOrganizations } from "./routingByOrganizations";

export class MenuByOrganizations {

    public static getMenuItems() {
        return [
            new MenuItemDef(RoutingByOrganizations.ORGANIZATIONS, 'Организации'),
        ];
    }
}