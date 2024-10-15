
export class Pagination<T> {

    constructor(public readonly items: T[],
        public readonly totalPages: number,
        public readonly total: number) { }
}