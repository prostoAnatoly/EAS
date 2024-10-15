import { ExTableSearchParams } from "../../../../../../../kit/tables/ExTable";
import { AnyObjectItemsSearch } from "../../../../../../../sharedModels/exTableView/AnyObjectItemsSearch";
import { EmployeeState } from "../../../../../domain/indexDomain";

export class GetEmployeesArgs extends AnyObjectItemsSearch {

	constructor(sParams: ExTableSearchParams) {
		super(sParams);
	}

	state?: EmployeeState;
}