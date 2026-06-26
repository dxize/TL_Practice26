import { useReducer, useEffect } from "react";
import { ApiClient } from "../../api/apiClient";
import { fetchCurrenciesReducer, initialState } from "./fetchCurrenciesReducer";

export const useFetchCurrenciesOnMount = () => {
    const [state, dispatch] = useReducer(fetchCurrenciesReducer, initialState);

    useEffect(() => {
        const controller = new AbortController();

        const loadCurrencies = async () => {
            dispatch({ type: "FETCH_START" });
            const { result, errorMessage } = await ApiClient.fetchCurrencies(controller.signal);

            if (controller.signal.aborted) {
                return;
            }

            if (errorMessage) {
                dispatch({ type: "FETCH_ERROR", payload: errorMessage });
            } else {
                dispatch({ type: "FETCH_SUCCESS", payload: result });
            }
        };

        loadCurrencies();

        return () => {
            controller.abort();
        };
    }, []);

    return state;
};
