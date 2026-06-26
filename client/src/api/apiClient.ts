import type { CurrencyDto } from "./dto/CurrencyDto";
import type { PriceChangeDto } from "./dto/PriceChangeDto";
import type { Currency, PriceChange } from "../models";
import { mapCurrencyDtoToModel } from "./mappers/currencyMapper";
import { mapPriceChangeDtoToModel } from "./mappers/priceChangeMapper";

const API_BASE_URL = "http://localhost:5081";
const MS_PER_MINUTE = 60_000;

type Result<TData> = {
    result: TData;
    errorMessage: string;
};

const getErrorMessage = (error: unknown): string => {
    if (error instanceof Error) {
        return error.message;
    }

    return `${error}`;
};

const fetchCurrencies = async (
    signal?: AbortSignal
): Promise<Result<Currency[]>> => {
    try {
        const response = await fetch(`${API_BASE_URL}/Currency`, { signal });
        if (!response.ok) {
            return {
                result: [],
                errorMessage: `Failed to fetch currencies, Status Code = ${response.status} ${response.statusText}`,
            };
        }

        const dtos: CurrencyDto[] = await response.json();

        return { result: dtos.map(mapCurrencyDtoToModel), errorMessage: "" };
    } catch (error: unknown) {
        return { result: [], errorMessage: getErrorMessage(error) };
    }
};

const fetchPriceHistory = async (
    fromCurrencyCode: string,
    toCurrencyCode: string,
    periodMinutes: number,
    signal?: AbortSignal
): Promise<Result<PriceChange[]>> => {
    const now = new Date();
    const from = new Date(now.getTime() - periodMinutes * MS_PER_MINUTE);
    const fromDateTime = encodeURIComponent(from.toISOString());

    try {
        const response = await fetch(
            `${API_BASE_URL}/prices?paymentCurrency=${fromCurrencyCode}&purchasedCurrency=${toCurrencyCode}&fromDateTime=${fromDateTime}`,
            { signal }
        );

        if (!response.ok) {
            return {
                result: [],
                errorMessage: `Failed to fetch price history for ${fromCurrencyCode}-${toCurrencyCode}, Status Code = ${response.status} ${response.statusText}`,
            };
        }

        const dtos: PriceChangeDto[] = await response.json();

        return { result: dtos.map(mapPriceChangeDtoToModel), errorMessage: "" };
    } catch (error: unknown) {
        return { result: [], errorMessage: getErrorMessage(error) };
    }
};

export const ApiClient = {
    fetchCurrencies,
    fetchPriceHistory,
};

export type ApiClient = typeof ApiClient;
