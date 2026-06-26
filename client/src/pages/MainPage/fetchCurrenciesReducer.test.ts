import { describe, test, expect } from "vitest";
import {
    fetchCurrenciesReducer,
    initialState,
    type FetchCurrenciesState,
} from "./fetchCurrenciesReducer";
import type { Currency } from "../../models";

describe("fetchCurrenciesReducer", () => {
    const mockCurrencies: Currency[] = [
        { code: "USD", name: "US Dollar", description: "", symbol: "$" },
        { code: "EUR", name: "Euro", description: "", symbol: "€" },
    ];

    test("FETCH_START sets isPending to true and clears error and result", () => {
        const prevState: FetchCurrenciesState = {
            isPending: false,
            errorMessage: "Old error",
            result: mockCurrencies,
        };

        const nextState = fetchCurrenciesReducer(prevState, { type: "FETCH_START" });

        expect(nextState).toEqual({
            isPending: true,
            errorMessage: "",
            result: undefined,
        });
    });

    test("FETCH_SUCCESS sets isPending to false and stores currencies", () => {
        const nextState = fetchCurrenciesReducer(initialState, {
            type: "FETCH_SUCCESS",
            payload: mockCurrencies,
        });

        expect(nextState).toEqual({
            isPending: false,
            errorMessage: "",
            result: mockCurrencies,
        });
    });

    test("FETCH_ERROR sets isPending to false and stores error message", () => {
        const nextState = fetchCurrenciesReducer(initialState, {
            type: "FETCH_ERROR",
            payload: "Network failure",
        });

        expect(nextState).toEqual({
            isPending: false,
            errorMessage: "Network failure",
            result: undefined,
        });
    });
});
