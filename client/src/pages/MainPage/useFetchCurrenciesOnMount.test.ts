import { describe, test, expect, vi, beforeEach } from "vitest";
import { renderHook, waitFor } from "@testing-library/react";
import { useFetchCurrenciesOnMount } from "./useFetchCurrenciesOnMount";
import * as apiClient from "../../api/apiClient";
import type { Currency } from "../../models";

vi.mock("../../api/apiClient", () => ({
    ApiClient: {
        fetchCurrencies: vi.fn(),
        fetchPriceHistory: vi.fn(),
    },
}));

const mockCurrencies: Currency[] = [
    { code: "CAD", name: "Canadian Dollar", description: "", symbol: "$" },
    { code: "PLN", name: "Polish Zloty", description: "", symbol: "zł" },
];

describe("useFetchCurrenciesOnMount", () => {
    beforeEach(() => {
        vi.clearAllMocks();
    });

    test("fetches currencies on mount successfully", async () => {
        vi.mocked(apiClient.ApiClient.fetchCurrencies).mockResolvedValue({
            result: mockCurrencies,
            errorMessage: "",
        });

        const { result } = renderHook(() => useFetchCurrenciesOnMount());

        expect(result.current.isPending).toBe(true);

        await waitFor(() => {
            expect(result.current.isPending).toBe(false);
        });

        expect(result.current.result).toEqual(mockCurrencies);
        expect(result.current.errorMessage).toBe("");
    });

    test("handles fetch error on mount", async () => {
        vi.mocked(apiClient.ApiClient.fetchCurrencies).mockResolvedValue({
            result: [],
            errorMessage: "Network failure",
        });

        const { result } = renderHook(() => useFetchCurrenciesOnMount());

        await waitFor(() => {
            expect(result.current.isPending).toBe(false);
        });

        expect(result.current.errorMessage).toBe("Network failure");
        expect(result.current.result).toBeUndefined();
    });
});
