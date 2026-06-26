import type { Currency } from "../../models";

export type FetchCurrenciesState = {
    isPending: boolean;
    errorMessage: string;
    result: Currency[] | undefined;
};

export type Action =
    | { type: "FETCH_START" }
    | { type: "FETCH_SUCCESS"; payload: Currency[] }
    | { type: "FETCH_ERROR"; payload: string };

export const initialState: FetchCurrenciesState = {
    isPending: true,
    errorMessage: "",
    result: undefined,
};

export const fetchCurrenciesReducer = (
    state: FetchCurrenciesState,
    action: Action
): FetchCurrenciesState => {
    switch (action.type) {
        case "FETCH_START":
            return { isPending: true, errorMessage: "", result: undefined };
        case "FETCH_SUCCESS":
            return { isPending: false, errorMessage: "", result: action.payload };
        case "FETCH_ERROR":
            return { isPending: false, errorMessage: action.payload, result: undefined };
        default:
            return state;
    }
};
