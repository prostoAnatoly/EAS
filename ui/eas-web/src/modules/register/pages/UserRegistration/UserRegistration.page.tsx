import * as React from 'react';
import { PageContent } from '../../../../kit/kit';
import { UserRegistrationForm } from './UserRegistration.form';

export function UserRegistrationPage() {
    return (<>
        <PageContent>
            <UserRegistrationForm />
        </PageContent>
    </>);
}