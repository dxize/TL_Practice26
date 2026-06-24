import { useReducer, useEffect, useState } from "react";
import { fetchCurrencies } from "../api/apiClient";
import { usePriceChart } from "./usePriceChart";
import { converterReducer, initialState } from "./converterReducer";

export type { ConverterState } from "./converterReducer";

const DEFAULT_PERIOD_MINUTES = 5;

export const useConverter = () => {
    const [state, dispatch] = useReducer(converterReducer, initialState);
    const [periodMinutes, setPeriodMinutes] = useState(DEFAULT_PERIOD_MINUTES);

    useEffect(() => {
        const controller = new AbortController();

        const loadCurrencies = async () => {
            dispatch({ type: "FETCH_CURRENCIES_START" });
            const { result, errorMessage } = await fetchCurrencies(controller.signal);
            if (controller.signal.aborted) {
                return;
            }
            if (errorMessage) {
                dispatch({ type: "FETCH_CURRENCIES_ERROR", payload: errorMessage });
            } else {
                dispatch({ type: "FETCH_CURRENCIES_SUCCESS", payload: result });
            }
        };

        loadCurrencies();

        return () => {
            controller.abort();
        };
    }, []);

    const { chartData, chartError, latestPrice } = usePriceChart(
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

    const chartLoading = chartData.length === 0 && !chartError;

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
