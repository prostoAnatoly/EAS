import * as React from 'react';
import { PageContent } from '../../../../kit/kit';
import { OrganizationsListForm } from './OrganizationsList.form';

export function OrganizationsListPage() {
    return (<>
        <PageContent>
            <OrganizationsListForm />
        </PageContent>
    </>);
}