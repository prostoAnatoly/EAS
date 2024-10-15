import { makeAutoObservable } from 'mobx';
import { Organization } from '../domain/indexDomain';

class OrganizationsStore {

	ogranizations: Organization[] | undefined = undefined;

	selectedOgranization: Organization | undefined = undefined;

	constructor() {
		makeAutoObservable(this);
	}

	public get hasOgranizations(): boolean {
		if (!this.ogranizations) { return false; }

		return this.ogranizations.length > 0;
	}
}

export const organizationsStore = new OrganizationsStore();