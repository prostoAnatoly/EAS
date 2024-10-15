import * as React from "react";
import { PageContent } from "../../../../kit/kit";
import { ForbiddenForm } from "./Forbidden.form";

export function ForbiddenPage() {
    return (
        <>
            <PageContent>
                <ForbiddenForm />
            </PageContent>
        </>
    );
}