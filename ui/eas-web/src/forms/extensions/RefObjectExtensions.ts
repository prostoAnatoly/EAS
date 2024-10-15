import { RefObject } from 'react';

export class RefObjectExtensions {

	public static getValue(refObject: RefObject<HTMLInputElement>): string {
		if (refObject && refObject.current) {
			return refObject.current.value;
		}
		return '';
	}

	public static setValue(refObject: RefObject<HTMLInputElement>, val: string): void {
		if (refObject && refObject.current) {
			refObject.current.value = val;
		}
	}

}