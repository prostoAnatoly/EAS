import { useState, useCallback, DependencyList, useRef, useEffect, useMemo } from 'react';
import { NavigateFunction, useLocation, useNavigate } from 'react-router';

export class ProcessingAgrs {
    public tag?: any;
}

export function useProcessingOnlyOne(func: (e: ProcessingAgrs) => Promise<void>,
    deps: DependencyList,
    errorHandler: ((err: unknown) => void) | undefined = undefined): [boolean, (params?: any) => Promise<void>] {

    const [isProcessing, setIsProcessing] = useStateSmart(false);

    const callback = useCallback(async (params?: any) => {
        if (isProcessing) { return };

        setIsProcessing(true);

        const args = new ProcessingAgrs();
        args.tag = params;
        try {
            await func(args);
        }
        catch (error) {
            console.log(error);
            if (errorHandler) {
                errorHandler(error);
            }
        }
        finally {
            setIsProcessing(false);
        }

    }, [isProcessing, ...deps]);

    return [isProcessing, callback];
}

export function usePathnames(): string[] {
    const location = useLocation();
    return location.pathname.split('/');
}

export function useMounted(funcEffect?: () => void) {
    const mounted = useRef(true);
    useEffect(() => {
        if (funcEffect) {
            funcEffect();
        }
        return () => { mounted.current = false };
    }, []);

    return mounted;
}

export function useStateSmart<T>(initialState: T | (() => T), funcEffect?: () => void): [T, (newState: T | ((prev: T) => T)) => void] {
    const [state, setState] = useState(initialState);
    const mounted = useMounted(funcEffect);

    const eventHandler = (newState: T | ((prev: T) => T)) => {
        if (mounted.current) {
            setState(newState);
        }
    };

    return [state, eventHandler];
}

export function useQuery() {
    const { search } = useLocation();

    return useMemo(() => new URLSearchParams(search), [search]);
}

export function useNavigateSearch () {
    const navigate = useNavigate();

    return useNavigateSearchWithNav(navigate);
};

export function useNavigateSearchWithNav(navigate: NavigateFunction) {
    return (pathname, params) =>
        navigate({ pathname, search: `?${new URLSearchParams(params)}` });
};