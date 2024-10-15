import { MaskControl, MaskControlProps } from '../Forms';

export class NumberControlProps extends MaskControlProps {
    numberOfDigits: number = 2;
}

export class NumberControl extends MaskControl {

    constructor(public props: NumberControlProps) {
        super(props, '', '');
        this.mask = this.getMask(props);
    }

    private getMask(props: NumberControlProps): string {
        let mask = '';
        for (let i = 0; i < props.numberOfDigits; i++) {
            mask += '9';
        }
        return mask;
    }

    protected override clean(val: string): string {
        if (val) {
            val = val.replace(/\D+/g, '');
        }
        return val;
    }

    public getNumber(): number {
        return Number.parseInt(this.getValue(), 10);
    }
}