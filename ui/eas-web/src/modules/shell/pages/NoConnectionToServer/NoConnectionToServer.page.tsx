import * as React from "react";
import { PageContent } from "../../../../kit/kit";
import { NoConnectionToServerForm } from "./NoConnectionToServer.form";

export function NoConnectionToServerPage() {
    return (
        <>
            <PageContent>
                <NoConnectionToServerForm />
            </PageContent>
        </>
    );
}