
export class FullName {
    constructor(
        public readonly name: string,
        public readonly surname: string,
        public readonly patronymic?: string) { }

    public ToString(): string {
        const s = this.surname || '';
        const f = this.name || '';
        if (this.patronymic) {
            return `${s} ${f} ${this.patronymic}`;
        }
        return `${s} ${f}`;
    }
}