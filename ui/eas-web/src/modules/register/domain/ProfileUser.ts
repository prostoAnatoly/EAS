import { FullName, Guid } from "../../../sharedModels/indexSharedModels";


export class ProfileUser {
	constructor(
		public fullName?: FullName,
		public avatarUrl?: string,
		public userName?: string) {
	}
}