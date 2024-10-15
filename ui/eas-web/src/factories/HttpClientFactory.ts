import { Token } from "../utils/Token";
import { HttpClient } from "../utils/web/HttpClient";

export class HttpClientFactory {

	private static headerVersionName = 'api-version';

	public static createHttpClient(version: string | null = '1.0', isAllowCanceling: boolean = false): HttpClient {
		const headers = new Map<string, string>();
		if (version) {
			headers.set(HttpClientFactory.headerVersionName, version);
		}

		return new HttpClient(null, isAllowCanceling, headers);
	}

	public static createHttpClientWithAuth(version: string | null = '1.0', isAllowCanceling: boolean = false): HttpClient {
		const headers = new Map<string, string>();
		if (version) {
			headers.set(HttpClientFactory.headerVersionName, version);
		}

		return new HttpClient(Token.get(), isAllowCanceling, headers);
	}

}