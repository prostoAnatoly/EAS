import * as React from 'react';
import { PageContent } from '../../../../kit/kit';
import { EmployeesListForm } from './EmployeesList.form';

export function EmployeesListPage() {
    return (<>
        <PageContent>
            <EmployeesListForm />
        </PageContent>
    </>);
}