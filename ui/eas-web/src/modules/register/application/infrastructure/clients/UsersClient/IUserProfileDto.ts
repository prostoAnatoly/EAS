import { IFullNameDto } from './IFullNameDto';

export interface IUserProfileDto {
	fullName: IFullNameDto;
	avatarUrl?: string;
	userName?: string;
}