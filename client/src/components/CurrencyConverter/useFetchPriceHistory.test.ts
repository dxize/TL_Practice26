import { describe, test, expect, vi, beforeEach } from "vitest";
import { renderHook, act, waitFor } from "@testing-library/react";
import { startTransition } from "react";
import { useFetchPriceHistory } from "./useFetchPriceHistory";
import * as apiClient from "../../api/apiClient";
import type { PriceChange } from "../../models";

vi.mock("../../api/apiClient", () => ({
    ApiClient: {
        fetchCurrencies: vi.fn(),
        fetchPriceHistory: vi.fn(),
    },
}));

const mockPrices: PriceChange[] = [
    {
        fromCurrencyCode: "CAD",
        toCurrencyCode: "PLN",
        price: 2.95,
        dateTime: "2026-05-21T03:40:04Z",
    },
];

describe("useFetchPriceHistory", () => {
    beforeEach(() => {
        vi.clearAllMocks();
    });

    test("fetches price history successfully via action", async () => {
        vi.mocked(apiClient.ApiClient.fetchPriceHistory).mockResolvedValue({
            result: mockPrices,
            errorMessage: "",
        });

        const { result } = renderHook(() => useFetchPriceHistory());

        const [_state, fetchHistory] = result.current;

        await act(async () => {
            startTransition(() => {
                fetchHistory({ fromCode: "CAD", toCode: "PLN", periodMinutes: 5 });
            });
        });

        await waitFor(() => {
            expect(result.current[0].result).toEqual(mockPrices);
        });

        expect(result.current[0].errorMessage).toBe("");
    });
});
