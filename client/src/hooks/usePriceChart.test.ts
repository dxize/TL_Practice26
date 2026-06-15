import { describe, test, expect, vi, beforeEach, afterEach } from "vitest";
import { renderHook, act } from "@testing-library/react";
import { usePriceChart } from "./usePriceChart";
import * as apiClient from "../api/apiClient";
import type { PriceChange } from "../models";

vi.mock("../api/apiClient", () => ({
    fetchCurrencies: vi.fn(),
    fetchPriceHistory: vi.fn(),
}));

const mockData: PriceChange[] = [
    {
        fromCurrencyCode: "CAD",
        toCurrencyCode: "PLN",
        price: 2.90,
        dateTime: "2026-05-21T03:40:44Z",
    },
    {
        fromCurrencyCode: "CAD",
        toCurrencyCode: "PLN",
        price: 2.95,
        dateTime: "2026-05-21T03:40:54Z",
    },
];

const REFRESH_INTERVAL = 10_000;

describe("usePriceChart", () => {
    beforeEach(() => {
        vi.useFakeTimers();
        vi.clearAllMocks();
    });

    afterEach(() => {
        vi.useRealTimers();
    });

    test("loads data on mount", async () => {
        vi.mocked(apiClient.fetchPriceHistory).mockResolvedValue(mockData);

        const { result } = renderHook(() => usePriceChart("CAD", "PLN", 5));

        expect(result.current.chartLoading).toBe(true);

        await act(async () => {
            await vi.advanceTimersByTimeAsync(0);
        });

        expect(result.current.chartLoading).toBe(false);
        expect(result.current.chartData).toEqual(mockData);
        expect(result.current.latestPrice).toEqual(mockData[1]);
        expect(result.current.chartError).toBeNull();
    });

    test("does not fetch when codes are empty", () => {
        const { result } = renderHook(() => usePriceChart("", "", 5));

        expect(apiClient.fetchPriceHistory).not.toHaveBeenCalled();
        expect(result.current.chartLoading).toBe(true);
    });

    test("auto-refreshes data by timer", async () => {
        vi.mocked(apiClient.fetchPriceHistory).mockResolvedValue(mockData);

        const { result } = renderHook(() => usePriceChart("CAD", "PLN", 5));

        await act(async () => {
            await vi.advanceTimersByTimeAsync(0);
        });

        expect(result.current.chartLoading).toBe(false);
        expect(apiClient.fetchPriceHistory).toHaveBeenCalledTimes(1);

        const updatedData: PriceChange[] = [
            ...mockData,
            {
                fromCurrencyCode: "CAD",
                toCurrencyCode: "PLN",
                price: 3.00,
                dateTime: "2026-05-21T03:41:04Z",
            },
        ];
        vi.mocked(apiClient.fetchPriceHistory).mockResolvedValue(updatedData);

        await act(async () => {
            await vi.advanceTimersByTimeAsync(REFRESH_INTERVAL);
        });

        expect(result.current.chartData).toEqual(updatedData);
        expect(apiClient.fetchPriceHistory).toHaveBeenCalledTimes(2);
    });

    test("refetches when currency pair changes", async () => {
        vi.mocked(apiClient.fetchPriceHistory).mockResolvedValue(mockData);

        const { result, rerender } = renderHook(
            ({ from, to }) => usePriceChart(from, to, 5),
            { initialProps: { from: "CAD", to: "PLN" } }
        );

        await act(async () => {
            await vi.advanceTimersByTimeAsync(0);
        });

        expect(result.current.chartLoading).toBe(false);

        const newData: PriceChange[] = [
            {
                fromCurrencyCode: "PLN",
                toCurrencyCode: "CAD",
                price: 0.34,
                dateTime: "2026-05-21T03:41:00Z",
            },
        ];
        vi.mocked(apiClient.fetchPriceHistory).mockResolvedValue(newData);

        rerender({ from: "PLN", to: "CAD" });

        await act(async () => {
            await vi.advanceTimersByTimeAsync(0);
        });

        expect(result.current.chartData).toEqual(newData);
    });

    test("refetches when period changes", async () => {
        vi.mocked(apiClient.fetchPriceHistory).mockResolvedValue(mockData);

        const { result, rerender } = renderHook(
            ({ period }) => usePriceChart("CAD", "PLN", period),
            { initialProps: { period: 5 } }
        );

        await act(async () => {
            await vi.advanceTimersByTimeAsync(0);
        });

        expect(result.current.chartLoading).toBe(false);

        vi.mocked(apiClient.fetchPriceHistory).mockResolvedValue([mockData[0]]);

        rerender({ period: 1 });

        await act(async () => {
            await vi.advanceTimersByTimeAsync(0);
        });

        expect(result.current.chartData).toHaveLength(1);
    });

    test("clears interval on unmount", async () => {
        vi.mocked(apiClient.fetchPriceHistory).mockResolvedValue(mockData);

        const { result, unmount } = renderHook(() => usePriceChart("CAD", "PLN", 5));

        await act(async () => {
            await vi.advanceTimersByTimeAsync(0);
        });

        expect(result.current.chartLoading).toBe(false);

        unmount();

        vi.mocked(apiClient.fetchPriceHistory).mockClear();

        await act(async () => {
            await vi.advanceTimersByTimeAsync(REFRESH_INTERVAL);
        });

        expect(apiClient.fetchPriceHistory).not.toHaveBeenCalled();
    });

    test("shows error on first load failure", async () => {
        vi.mocked(apiClient.fetchPriceHistory).mockRejectedValue(
            new Error("Network Error")
        );

        const { result } = renderHook(() => usePriceChart("CAD", "PLN", 5));

        await act(async () => {
            await vi.advanceTimersByTimeAsync(0);
        });

        expect(result.current.chartLoading).toBe(false);
        expect(result.current.chartError).toBe("Network Error");
        expect(result.current.chartData).toEqual([]);
    });

    test("keeps old data on auto-refresh error", async () => {
        vi.mocked(apiClient.fetchPriceHistory).mockResolvedValue(mockData);

        const { result } = renderHook(() => usePriceChart("CAD", "PLN", 5));

        await act(async () => {
            await vi.advanceTimersByTimeAsync(0);
        });

        expect(result.current.chartData).toEqual(mockData);

        vi.mocked(apiClient.fetchPriceHistory).mockRejectedValue(
            new Error("Refresh failed")
        );

        await act(async () => {
            await vi.advanceTimersByTimeAsync(REFRESH_INTERVAL);
        });

        expect(result.current.chartError).toBe("Refresh failed");
        expect(result.current.chartData).toEqual(mockData);
    });

    test("handles empty API response", async () => {
        vi.mocked(apiClient.fetchPriceHistory).mockResolvedValue([]);

        const { result } = renderHook(() => usePriceChart("CAD", "PLN", 5));

        await act(async () => {
            await vi.advanceTimersByTimeAsync(0);
        });

        expect(result.current.chartLoading).toBe(false);
        expect(result.current.chartData).toEqual([]);
        expect(result.current.latestPrice).toBeNull();
        expect(result.current.chartError).toBeNull();
    });

    test("old request does not overwrite new data (race condition)", async () => {
        let resolveFirst: ((value: PriceChange[]) => void) | null = null;
        const firstPromise = new Promise<PriceChange[]>((resolve) => {
            resolveFirst = resolve;
        });

        vi.mocked(apiClient.fetchPriceHistory).mockReturnValueOnce(firstPromise);

        const { result, rerender } = renderHook(
            ({ from, to }) => usePriceChart(from, to, 5),
            { initialProps: { from: "CAD", to: "PLN" } }
        );

        const newData: PriceChange[] = [
            {
                fromCurrencyCode: "PLN",
                toCurrencyCode: "CAD",
                price: 0.34,
                dateTime: "2026-05-21T03:41:00Z",
            },
        ];
        vi.mocked(apiClient.fetchPriceHistory).mockResolvedValue(newData);

        rerender({ from: "PLN", to: "CAD" });

        await act(async () => {
            await vi.advanceTimersByTimeAsync(0);
        });

        expect(result.current.chartData).toEqual(newData);

        await act(async () => {
            resolveFirst!(mockData);
        });

        expect(result.current.chartData).toEqual(newData);
    });
});
