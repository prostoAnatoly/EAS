import { LocalCache } from "./LocalCache";

export abstract class CacheBase<T> {

	constructor(protected readonly storageKey: string) {

	}

	public save(value: T) {
		LocalCache.Push(this.storageKey, value, undefined);
	}

	public get(): T | undefined {
		return LocalCache.Get(this.storageKey);
	}

	public delete() {
		LocalCache.Remove(this.storageKey);
	}

}