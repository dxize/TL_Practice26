import { useReducer, useEffect } from "react";
import type { Currency, PriceChange } from "../models";
import { fetchCurrencies, fetchPriceChange } from "../api/apiClient";

export type ConverterState = {
    currencies: Currency[];
    priceChange: PriceChange | null;
    isLoading: boolean;
    error: string | null;
    toastError: string | null;
    fromCode: string;
    toCode: string;
    amount: string;
};

type Action =
    | { type: "FETCH_CURRENCIES_START" }
    | { type: "FETCH_CURRENCIES_SUCCESS"; payload: Currency[] }
    | { type: "FETCH_CURRENCIES_ERROR"; payload: string }
    | { type: "FETCH_PRICE_START" }
    | { type: "FETCH_PRICE_SUCCESS"; payload: PriceChange }
    | { type: "FETCH_PRICE_ERROR"; payload: string }
    | { type: "SET_AMOUNT"; payload: string }
    | { type: "SET_FROM_CODE"; payload: string }
    | { type: "SET_TO_CODE"; payload: string }
    | { type: "SWAP" };

const initialState: ConverterState = {
    currencies: [],
    priceChange: null,
    isLoading: true,
    error: null,
    toastError: null,
    fromCode: "",
    toCode: "",
    amount: "1",
};

const findAlternative = (currencies: Currency[], excludeCode: string): string => {
    const alt = currencies.find((c) => c.code !== excludeCode);
    return alt ? alt.code : excludeCode;
};

const converterReducer = (state: ConverterState, action: Action): ConverterState => {
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

        case "FETCH_PRICE_START":
            return { ...state, toastError: null };
        case "FETCH_PRICE_SUCCESS":
            return { ...state, priceChange: action.payload, toastError: null };
        case "FETCH_PRICE_ERROR":
            return { ...state, toastError: action.payload };

        case "SET_AMOUNT":
            return { ...state, amount: action.payload };
        case "SET_FROM_CODE":
            return {
                ...state,
                fromCode: action.payload,
                toCode: action.payload === state.toCode
                    ? findAlternative(state.currencies, action.payload)
                    : state.toCode
            };
        case "SET_TO_CODE":
            return {
                ...state,
                toCode: action.payload,
                fromCode: action.payload === state.fromCode
                    ? findAlternative(state.currencies, action.payload)
                    : state.fromCode
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

export const useConverter = () => {
    const [state, dispatch] = useReducer(converterReducer, initialState);

    useEffect(() => {
        let isMounted = true;

        const loadCurrencies = async () => {
            dispatch({ type: "FETCH_CURRENCIES_START" });
            try {
                const data = await fetchCurrencies();
                if (isMounted) {
                    dispatch({ type: "FETCH_CURRENCIES_SUCCESS", payload: data });
                }
            } catch (err: any) {
                if (isMounted) {
                    dispatch({ type: "FETCH_CURRENCIES_ERROR", payload: err.message });
                }
            }
        };

        loadCurrencies();

        return () => {
            isMounted = false;
        };
    }, []);

    useEffect(() => {
        let isMounted = true;

        if (!state.fromCode || !state.toCode) return;

        const loadPrice = async () => {
            dispatch({ type: "FETCH_PRICE_START" });
            try {
                const data = await fetchPriceChange(state.fromCode, state.toCode);
                if (isMounted) {
                    dispatch({ type: "FETCH_PRICE_SUCCESS", payload: data });
                }
            } catch (err: any) {
                if (isMounted) {
                    dispatch({ type: "FETCH_PRICE_ERROR", payload: err.message });
                }
            }
        };

        loadPrice();

        return () => {
            isMounted = false;
        };
    }, [state.fromCode, state.toCode]);

    const setAmount = (val: string) => dispatch({ type: "SET_AMOUNT", payload: val });
    const handleFromChange = (code: string) => dispatch({ type: "SET_FROM_CODE", payload: code });
    const handleToChange = (code: string) => dispatch({ type: "SET_TO_CODE", payload: code });
    const swap = () => dispatch({ type: "SWAP" });

    const numericAmount = parseFloat(state.amount.replace(",", "."));
    const result =
        isNaN(numericAmount) || !state.priceChange
            ? ""
            : (numericAmount * state.priceChange.price).toFixed(2);

    const fromCurrency = state.currencies.find((c) => c.code === state.fromCode) ?? null;
    const toCurrency = state.currencies.find((c) => c.code === state.toCode) ?? null;

    const pairKey = `${state.fromCode}-${state.toCode}`;

    return {
        state,
        result,
        fromCurrency,
        toCurrency,
        pairKey,
        setAmount,
        handleFromChange,
        handleToChange,
        swap,
    };
};

export { converterReducer, initialState };
