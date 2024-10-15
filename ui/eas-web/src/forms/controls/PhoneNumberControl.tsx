import { MaskControl, MaskControlProps } from '../Forms';

export class PhoneNumberControlProps extends MaskControlProps {

}

export class PhoneNumberControl extends MaskControl {

    constructor(public props: PhoneNumberControlProps) {
        super(props, '+7 999 999 99 99', ' ');
    }

    protected override clean(val: string): string {
        if (val) {
            val = val.replace(/\D+/g, '');
        }
        return val;
    }

}