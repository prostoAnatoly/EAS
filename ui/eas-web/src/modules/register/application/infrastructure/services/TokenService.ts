import { Token } from "../../../../../utils/Token";

export class TokenService {

    clear() {
        Token.clear();
    }

    create(accessToken: string) {
        const token = new Token();
        token.save(accessToken);
    }
}