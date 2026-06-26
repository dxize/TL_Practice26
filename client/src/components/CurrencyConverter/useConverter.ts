import { useState, useEffect, startTransition } from "react";
import type { Currency } from "../../models";
import { useFetchPriceHistory } from "./useFetchPriceHistory";

const DEFAULT_PERIOD_MINUTES = 5;
const REFRESH_INTERVAL_MS = 10_000;

const findAlternative = (currencies: Currency[], excludeCode: string): string => {
    const alt = currencies.find((c) => c.code !== excludeCode);
    return alt ? alt.code : excludeCode;
};

export const useConverter = (currencies: Currency[]) => {
    const [amount, setAmount] = useState("1");
    const [fromCode, setFromCode] = useState(currencies[0]?.code ?? "");
    const [toCode, setToCode] = useState(
        currencies[1]?.code ?? currencies[0]?.code ?? ""
    );
    const [periodMinutes, setPeriodMinutes] = useState(DEFAULT_PERIOD_MINUTES);

    const [priceState, fetchPriceHistory, isPending] = useFetchPriceHistory();

    useEffect(() => {
        if (!fromCode || !toCode) return;

        startTransition(() => {
            fetchPriceHistory({ fromCode, toCode, periodMinutes });
        });

        const intervalId = setInterval(() => {
            startTransition(() => {
                fetchPriceHistory({ fromCode, toCode, periodMinutes });
            });
        }, REFRESH_INTERVAL_MS);

        return () => clearInterval(intervalId);
    }, [fromCode, toCode, periodMinutes, fetchPriceHistory]);

    const handleFromChange = (code: string) => {
        setFromCode(code);
        if (code === toCode) {
            setToCode(findAlternative(currencies, code));
        }
    };

    const handleToChange = (code: string) => {
        setToCode(code);
        if (code === fromCode) {
            setFromCode(findAlternative(currencies, code));
        }
    };

    const swap = () => {
        setFromCode(toCode);
        setToCode(fromCode);
    };

    const chartData = priceState.result;
    const chartError = priceState.errorMessage ? priceState.errorMessage : null;
    const latestPrice = chartData.length > 0 ? chartData[chartData.length - 1] : null;

    const numericAmount = parseFloat(amount.replace(",", "."));
    const result =
        isNaN(numericAmount) || !latestPrice
            ? ""
            : (numericAmount * latestPrice.price).toFixed(2);

    const fromCurrency = currencies.find((c) => c.code === fromCode) ?? null;
    const toCurrency = currencies.find((c) => c.code === toCode) ?? null;

    const pairKey = `${fromCode}-${toCode}`;
    const chartLoading = isPending && chartData.length === 0 && !chartError;

    const state = {
        amount,
        fromCode,
        toCode,
        currencies,
    };

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
