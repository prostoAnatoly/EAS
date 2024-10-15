import { makeAutoObservable } from "mobx";
import { CurrentProfileUserCache } from "../CurrentProfileUserCache";
import { ProfileUser } from "../domain/indexDomain";

class CurrentProfileUserStore {

	private static readonly cache = new CurrentProfileUserCache();
	private _profile: ProfileUser | undefined = undefined;

	constructor() {
		this._profile = CurrentProfileUserStore.cache.get();
        makeAutoObservable(this);
    }

	public get Profile() {
		return this._profile;
	}

	public set Profile(profile: ProfileUser | undefined) {
		this._profile = profile;
		if (profile) {
			CurrentProfileUserStore.cache.save(profile);
		}
		else {
			CurrentProfileUserStore.cache.delete();
		}
	}
}

export const currentProfileUserStore = new CurrentProfileUserStore();