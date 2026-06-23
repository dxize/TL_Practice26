import type { Currency } from "../models";

export type ConverterState = {
    currencies: Currency[];
    isLoading: boolean;
    error: string | null;
    fromCode: string;
    toCode: string;
    amount: string;
};

export type Action =
    | { type: "FETCH_CURRENCIES_START" }
    | { type: "FETCH_CURRENCIES_SUCCESS"; payload: Currency[] }
    | { type: "FETCH_CURRENCIES_ERROR"; payload: string }
    | { type: "SET_AMOUNT"; payload: string }
    | { type: "SET_FROM_CODE"; payload: string }
    | { type: "SET_TO_CODE"; payload: string }
    | { type: "SWAP" };

export const initialState: ConverterState = {
    currencies: [],
    isLoading: true,
    error: null,
    fromCode: "",
    toCode: "",
    amount: "1",
};

const findAlternative = (currencies: Currency[], excludeCode: string): string => {
    const alt = currencies.find((c) => c.code !== excludeCode);
    return alt ? alt.code : excludeCode;
};

export const converterReducer = (state: ConverterState, action: Action): ConverterState => {
    switch (action.type) {
        case "FETCH_CURRENCIES_START":
            return { ...state, isLoading: true, error: null };
        case "FETCH_CURRENCIES_SUCCESS":
            return {
                ...state,
                isLoading: false,
                currencies: action.payload,
                fromCode: action.payload[0]?.code ?? "",
                toCode: action.payload[1]?.code ?? action.payload[0]?.code ?? "",
            };
        case "FETCH_CURRENCIES_ERROR":
            return { ...state, isLoading: false, error: action.payload };

        case "SET_AMOUNT":
            return { ...state, amount: action.payload };
        case "SET_FROM_CODE":
            return {
                ...state,
                fromCode: action.payload,
                toCode: action.payload === state.toCode
                    ? findAlternative(state.currencies, action.payload)
                    : state.toCode,
            };
        case "SET_TO_CODE":
            return {
                ...state,
                toCode: action.payload,
                fromCode: action.payload === state.fromCode
                    ? findAlternative(state.currencies, action.payload)
                    : state.fromCode,
            };
        case "SWAP":
            return {
                ...state,
                fromCode: state.toCode,
                toCode: state.fromCode,
            };
        default:
            return state;
    }
};
