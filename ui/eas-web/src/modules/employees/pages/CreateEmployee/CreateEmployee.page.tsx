import * as React from 'react';
import { PageContent } from '../../../../kit/kit';
import { CreateEmployeeForm } from './CreateEmployee.form';

export function CreateEmployeePage() {
    return (<>
        <PageContent>
            <CreateEmployeeForm />
        </PageContent>
    </>);
}