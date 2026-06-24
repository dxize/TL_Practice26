import type { CurrencyDto } from "./dto/CurrencyDto";
import type { PriceChangeDto } from "./dto/PriceChangeDto";
import type { Currency, PriceChange } from "../models";
import { mapCurrencyDtoToModel } from "./mappers/currencyMapper";
import { mapPriceChangeDtoToModel } from "./mappers/priceChangeMapper";

const API_BASE_URL = "http://localhost:5081";
const MS_PER_MINUTE = 60_000;

export type Result<TData> = {
    result: TData;
    errorMessage: string;
};

export const fetchCurrencies = async (
    signal?: AbortSignal
): Promise<Result<Currency[]>> => {
    const response = await fetch(`${API_BASE_URL}/Currency`, { signal });

    if (!response.ok) {
        let errorMessage = "Failed to fetch currencies";
        try {
            const errorData = await response.json();
            if (errorData && errorData.message) {
                errorMessage = errorData.message;
            }
        } catch {
        }
        return { result: [], errorMessage };
    }

    const dtos: CurrencyDto[] = await response.json();
    return { result: dtos.map(mapCurrencyDtoToModel), errorMessage: "" };
};

export const fetchPriceHistory = async (
    fromCurrencyCode: string,
    toCurrencyCode: string,
    periodMinutes: number,
    signal?: AbortSignal
): Promise<Result<PriceChange[]>> => {
    const now = new Date();
    const from = new Date(now.getTime() - periodMinutes * MS_PER_MINUTE);
    const fromDateTime = encodeURIComponent(from.toISOString());

    const response = await fetch(
        `${API_BASE_URL}/prices?paymentCurrency=${fromCurrencyCode}&purchasedCurrency=${toCurrencyCode}&fromDateTime=${fromDateTime}`,
        { signal }
    );

    if (!response.ok) {
        let errorMessage = `Failed to fetch price history for ${fromCurrencyCode}-${toCurrencyCode}`;
        try {
            const errorData = await response.json();
            if (errorData && errorData.message) {
                errorMessage = errorData.message;
            }
        } catch {
        }
        return { result: [], errorMessage };
    }

    const dtos: PriceChangeDto[] = await response.json();
    return { result: dtos.map(mapPriceChangeDtoToModel), errorMessage: "" };
};
