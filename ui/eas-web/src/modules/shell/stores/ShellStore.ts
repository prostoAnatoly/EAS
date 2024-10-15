import { makeObservable } from "mobx";

class ShellStore {

    public avatarObjectURL: string | undefined = undefined;

    constructor() {
        makeObservable(this);
    }
}

export const shellStore = new ShellStore();