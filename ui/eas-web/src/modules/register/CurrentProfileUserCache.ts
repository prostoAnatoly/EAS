import { CacheBase } from "../../utils/caches/CacheBase";
import { ProfileUser } from "./domain/indexDomain";

export class CurrentProfileUserCache extends CacheBase<ProfileUser> {

	// Константы
	private static readonly STORAGE_KEY: string = 'eas_public_profile';

	constructor() {
		super(CurrentProfileUserCache.STORAGE_KEY);
	}
}