import { useState, useEffect } from "react";
import type { PriceChange } from "../models";
import { fetchPriceHistory } from "../api/apiClient";

const REFRESH_INTERVAL_MS = 10_000;

export const usePriceChart = (
    fromCode: string,
    toCode: string,
    periodMinutes: number
) => {
    const [chartData, setChartData] = useState<PriceChange[]>([]);
    const [chartLoading, setChartLoading] = useState(true);
    const [chartError, setChartError] = useState<string | null>(null);

    useEffect(() => {
        if (!fromCode || !toCode) {
            return;
        }

        const controller = new AbortController();
        let isFirstLoad = true;

        const loadPrices = async () => {
            if (isFirstLoad) {
                setChartLoading(true);
                setChartError(null);
            }

            try {
                const data = await fetchPriceHistory(
                    fromCode,
                    toCode,
                    periodMinutes,
                    controller.signal
                );

                if (!controller.signal.aborted) {
                    setChartData(data);
                    setChartLoading(false);
                    setChartError(null);
                    isFirstLoad = false;
                }
            } catch (err: unknown) {
                if (controller.signal.aborted) {
                    return;
                }

                const message =
                    err instanceof Error ? err.message : "Unknown error";

                if (isFirstLoad) {
                    setChartData([]);
                    setChartLoading(false);
                    setChartError(message);
                } else {
                    setChartError(message);
                }
            }
        };

        loadPrices();

        const intervalId = setInterval(() => {
            loadPrices();
        }, REFRESH_INTERVAL_MS);

        return () => {
            controller.abort();
            clearInterval(intervalId);
        };
    }, [fromCode, toCode, periodMinutes]);

    const latestPrice =
        chartData.length > 0
            ? chartData[chartData.length - 1]
            : null;

    return {
        chartData,
        chartLoading,
        chartError,
        latestPrice,
    };
};
