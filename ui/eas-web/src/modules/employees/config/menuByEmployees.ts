import { MenuItemDef } from "../../../kit/kit";
import { RoutingByEmployees } from "./routingByEmployees";

export class MenuByEmployees {

    public static getMenuItems() {
        return [
            new MenuItemDef(RoutingByEmployees.EMPLOYEES, 'Сотрудники'),
        ];
    }
}