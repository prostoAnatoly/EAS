import * as React from 'react';
import { PageContent } from '../../../../kit/kit';
import { CreateOrganizationForm } from './CreateOrganization.form';

export function CreateOrganizationPage() {
    return (<>
        <PageContent>
            <CreateOrganizationForm />
        </PageContent>
    </>);
}