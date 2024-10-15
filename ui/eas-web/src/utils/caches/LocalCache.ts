

class CacheItem<TValue> {
	constructor(public value: TValue, public expPerSecond: number | undefined = undefined) { }
}

export class LocalCache {

	public static Get<T>(key: string): T | undefined {
		const item = localStorage.getItem(key);

		let cacheItem: CacheItem<T> | undefined = undefined;

		if (item) {
			try {
				cacheItem = JSON.parse(item);
				if (cacheItem) {
					if (cacheItem.expPerSecond && Date.now() >= cacheItem.expPerSecond) {
						cacheItem = undefined;
					}
				}
			}
			catch (err) {
				cacheItem = undefined;
			}
		}

		return cacheItem?.value;
	}

	public static Push<T>(key: string, item: T, expPerSecond: number | undefined = undefined) {
		if (expPerSecond) {
			expPerSecond = Date.now() + expPerSecond * 1000;
		}
		const cacheItem = new CacheItem(item, expPerSecond);
		localStorage.setItem(key, JSON.stringify(cacheItem));
	}

	public static Remove(key: string) {
		localStorage.removeItem(key);
	}
}