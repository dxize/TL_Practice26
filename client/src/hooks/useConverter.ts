import { useReducer, useEffect, useState } from "react";
import type { Currency } from "../models";
import { fetchCurrencies } from "../api/apiClient";
import { usePriceChart } from "./usePriceChart";

const DEFAULT_PERIOD_MINUTES = 5;

export type ConverterState = {
    currencies: Currency[];
    isLoading: boolean;
    error: string | null;
    fromCode: string;
    toCode: string;
    amount: string;
};

type Action =
    | { type: "FETCH_CURRENCIES_START" }
    | { type: "FETCH_CURRENCIES_SUCCESS"; payload: Currency[] }
    | { type: "FETCH_CURRENCIES_ERROR"; payload: string }
    | { type: "SET_AMOUNT"; payload: string }
    | { type: "SET_FROM_CODE"; payload: string }
    | { type: "SET_TO_CODE"; payload: string }
    | { type: "SWAP" };

const initialState: ConverterState = {
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
    const [periodMinutes, setPeriodMinutes] = useState(DEFAULT_PERIOD_MINUTES);

    useEffect(() => {
        const controller = new AbortController();

        const loadCurrencies = async () => {
            dispatch({ type: "FETCH_CURRENCIES_START" });
            try {
                const data = await fetchCurrencies(controller.signal);
                if (!controller.signal.aborted) {
                    dispatch({ type: "FETCH_CURRENCIES_SUCCESS", payload: data });
                }
            } catch (err: unknown) {
                if (!controller.signal.aborted) {
                    const message = err instanceof Error ? err.message : "Unknown error";
                    dispatch({ type: "FETCH_CURRENCIES_ERROR", payload: message });
                }
            }
        };

        loadCurrencies();

        return () => {
            controller.abort();
        };
    }, []);

    const { chartData, chartLoading, chartError, latestPrice } = usePriceChart(
        state.fromCode,
        state.toCode,
        periodMinutes
    );

    const setAmount = (val: string) => dispatch({ type: "SET_AMOUNT", payload: val });
    const handleFromChange = (code: string) => dispatch({ type: "SET_FROM_CODE", payload: code });
    const handleToChange = (code: string) => dispatch({ type: "SET_TO_CODE", payload: code });
    const swap = () => dispatch({ type: "SWAP" });

    const numericAmount = parseFloat(state.amount.replace(",", "."));
    const result =
        isNaN(numericAmount) || !latestPrice
            ? ""
            : (numericAmount * latestPrice.price).toFixed(2);

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
        latestPrice,
        chartData,
        chartLoading,
        chartError,
        periodMinutes,
        setPeriodMinutes,
    };
};

export { converterReducer, initialState };
