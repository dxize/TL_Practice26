import { useActionState } from "react";
import { ApiClient } from "../../api/apiClient";
import type { PriceChange } from "../../models";

type PriceHistoryState = {
    errorMessage: string;
    result: PriceChange[];
};

type PriceHistoryPayload = {
    fromCode: string;
    toCode: string;
    periodMinutes: number;
};

export const useFetchPriceHistory = () => {
    return useActionState<PriceHistoryState, PriceHistoryPayload>(
        async (_prevState, { fromCode, toCode, periodMinutes }) => {
            if (!fromCode || !toCode) {
                return { errorMessage: "", result: [] };
            }

            const { result, errorMessage } = await ApiClient.fetchPriceHistory(
                fromCode,
                toCode,
                periodMinutes
            );

            return {
                result,
                errorMessage,
            };
        },
        { errorMessage: "", result: [] }
    );
};
