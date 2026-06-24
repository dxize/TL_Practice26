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
    const [chartError, setChartError] = useState<string | null>(null);

    useEffect(() => {
        if (!fromCode || !toCode) {
            return;
        }

        setChartData([]);
        setChartError(null);

        const controller = new AbortController();

        const loadPrices = async () => {
            const { result, errorMessage } = await fetchPriceHistory(
                fromCode,
                toCode,
                periodMinutes,
                controller.signal
            );

            if (controller.signal.aborted) {
                return;
            }

            if (errorMessage) {
                setChartError(errorMessage);
            } else {
                setChartData(result);
                setChartError(null);
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
        chartError,
        latestPrice,
    };
};

