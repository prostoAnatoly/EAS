import { HttpClientFactory } from "../../../../factories/HttpClientFactory";
import { CreateOrganizationArgs } from "../../application/infrastructure/clients/OrganizationsClient/arguments/CreateOrganizationArgs";
import { IOrganizationsClient } from "../../application/infrastructure/clients/OrganizationsClient/IOrganizationsClient";
import { ICreateOrganizationResponse } from "../../application/infrastructure/clients/OrganizationsClient/responses/ICreateOrganizationResponse";
import { IGetOrganizationsResponse } from "../../application/infrastructure/clients/OrganizationsClient/responses/IGetOrganizationsResponse";

export class OrganizationsClient implements IOrganizationsClient {

    private static readonly baseUrl = '/api/organizations';

    createOrganization(args: CreateOrganizationArgs): Promise<ICreateOrganizationResponse> {

        const url = `${OrganizationsClient.baseUrl}/create`;

        const httpClient = HttpClientFactory.createHttpClientWithAuth();

        return httpClient.post<ICreateOrganizationResponse>(url, args);
    }

    getOrganizations(): Promise<IGetOrganizationsResponse> {
        const url = OrganizationsClient.baseUrl;

        const httpClient = HttpClientFactory.createHttpClientWithAuth();

        return httpClient.get<IGetOrganizationsResponse>(url);
    }

}