
export class Jwt {
    exp: number = 0;
}

export class Token {
    // Константы
    private static readonly TOKEN_STORAGE_KEY: string = 'eas_public_tokenKey';

    // кэш
    private static jwtInfo: Jwt | null = null;

    private static getJwtInfoByToken(token: string): Jwt | null {
        const payload = this.getPayloadFromToken(token);
        if (payload) {
            const result: any = new Jwt();
            const jwt = JSON.parse(payload);
            for (let prop in jwt) {
                if (result.hasOwnProperty(prop)) {
                    result[prop] = jwt[prop];
                }
            }
            return result;
        }
        return null;
    }

    private static getPayloadFromToken(token: string): string | null {
        const parts = token.split('.');
        if (parts.length === 3) {
            const base64Url = token.split('.')[1];
            if (base64Url) {
                const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
                const payload = decodeURIComponent(window.atob(base64).split('').map(function (c) {
                    return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
                }).join(''));

                return payload;
            }
        }
        return null;
    }

    public save(accessToken: string) {
        if (accessToken) {
            Token.clear();
            Token.jwtInfo = Token.getJwtInfoByToken(accessToken);
            localStorage.setItem(Token.TOKEN_STORAGE_KEY, accessToken);
        }
    }

    public static get(): string {
        const item = localStorage.getItem(Token.TOKEN_STORAGE_KEY);
        if (item) {
            return item;
        }
        return '';
    }

    public static clear() {
        // очищаем хранилище
        localStorage.removeItem(Token.TOKEN_STORAGE_KEY);
        // очищаем кэш
        Token.jwtInfo = null;
    }

    private static getJwtInfo(): Jwt | null {
        if (Token.jwtInfo === null) {
            const token = Token.get();
            if (token) {
                Token.jwtInfo = Token.getJwtInfoByToken(token);
            }
        }
        return Token.jwtInfo;
    }

    public static getIsExpired(): boolean {
        const jwt = Token.getJwtInfo();
        return jwt === null || (Date.now() >= jwt.exp * 1000);
    }
}