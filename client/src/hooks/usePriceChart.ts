import { useState, useEffect, useRef } from "react";
import type { PriceChange } from "../models";
import { fetchPriceHistory } from "../api/apiClient";

const REFRESH_INTERVAL_MS = 10_000;

type PriceChartState = {
    chartData: PriceChange[];
    chartLoading: boolean;
    chartError: string | null;
};

export const usePriceChart = (
    fromCode: string,
    toCode: string,
    periodMinutes: number
) => {
    const [state, setState] = useState<PriceChartState>({
        chartData: [],
        chartLoading: true,
        chartError: null,
    });

    const hasDataRef = useRef(false);

    useEffect(() => {
        if (!fromCode || !toCode) {
            return;
        }

        const controller = new AbortController();
        let intervalId: ReturnType<typeof setInterval> | null = null;

        const loadPrices = async (isAutoRefresh: boolean) => {
            if (!isAutoRefresh) {
                setState((prev) => ({
                    ...prev,
                    chartLoading: true,
                    chartError: null,
                }));
                hasDataRef.current = false;
            }

            try {
                const data = await fetchPriceHistory(
                    fromCode,
                    toCode,
                    periodMinutes,
                    controller.signal
                );

                if (controller.signal.aborted) {
                    return;
                }

                hasDataRef.current = data.length > 0;
                setState({
                    chartData: data,
                    chartLoading: false,
                    chartError: null,
                });
            } catch (err: unknown) {
                if (controller.signal.aborted) {
                    return;
                }

                const message =
                    err instanceof Error ? err.message : "Unknown error";

                if (isAutoRefresh && hasDataRef.current) {
                    setState((prev) => ({
                        ...prev,
                        chartError: message,
                    }));
                } else {
                    setState({
                        chartData: [],
                        chartLoading: false,
                        chartError: message,
                    });
                }
            }
        };

        loadPrices(false);

        intervalId = setInterval(() => {
            loadPrices(true);
        }, REFRESH_INTERVAL_MS);

        return () => {
            controller.abort();
            if (intervalId !== null) {
                clearInterval(intervalId);
            }
        };
    }, [fromCode, toCode, periodMinutes]);

    const latestPrice =
        state.chartData.length > 0
            ? state.chartData[state.chartData.length - 1]
            : null;

    return {
        chartData: state.chartData,
        chartLoading: state.chartLoading,
        chartError: state.chartError,
        latestPrice,
    };
};
