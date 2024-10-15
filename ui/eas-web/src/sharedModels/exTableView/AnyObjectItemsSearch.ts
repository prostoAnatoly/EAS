import { ExTableSearchParams, TOrder } from "../../kit/tables/ExTable";

export abstract class AnyObjectItemsSearch {
    pageSize: number;
    page: number;
    sort: SortInfo;
    filters: FilterInfo[];

    constructor(sParams: ExTableSearchParams) {
        this.pageSize = sParams.rowsPerPage;
        this.page = sParams.page;
        this.sort = new SortInfo(sParams.order, sParams.orderBy);
        this.filters = sParams.filters.map((f) => { return new FilterInfo(f.value, f.propertyName) });
    }
}

class SortInfo {
    sortType: SortTypeEnum;
    orderBy: string;
    constructor(order: TOrder, orderBy: string) {
        this.sortType = order == 'asc' ? SortTypeEnum.Asc : SortTypeEnum.Desc;
        this.orderBy = orderBy;
    }
}

enum SortTypeEnum { Asc, Desc };

class FilterInfo {
    constructor(public value: string, public columnName: string) { }
}