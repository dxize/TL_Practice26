import type { CurrencyDto } from "./dto/CurrencyDto";
import type { PriceChangeDto } from "./dto/PriceChangeDto";
import type { Currency, PriceChange } from "../models";
import { mapCurrencyDtoToModel } from "./mappers/currencyMapper";
import { mapPriceChangeDtoToModel } from "./mappers/priceChangeMapper";

const API_BASE_URL = "http://localhost:5081";

export const fetchCurrencies = async (): Promise<Currency[]> => {
    const response = await fetch(`${API_BASE_URL}/Currency`);

    if (!response.ok) {
        let errorMessage = "Failed to fetch currencies";
        try {
            const errorData = await response.json();
            if (errorData && errorData.message) {
                errorMessage = errorData.message;
            }
        } catch {

        }
        throw new Error(errorMessage);
    }

    const dtos: CurrencyDto[] = await response.json();
    return dtos.map(mapCurrencyDtoToModel);
};

export const fetchPriceChange = async (
    fromCurrencyCode: string,
    toCurrencyCode: string
): Promise<PriceChange> => {
    const fromDateTime = encodeURIComponent("2000-01-01T00:00:00Z");

    const response = await fetch(
        `${API_BASE_URL}/prices?paymentCurrency=${fromCurrencyCode}&purchasedCurrency=${toCurrencyCode}&fromDateTime=${fromDateTime}`
    );

    if (!response.ok) {
        let errorMessage = `Failed to fetch price change for ${fromCurrencyCode}-${toCurrencyCode}`;
        try {
            const errorData = await response.json();
            if (errorData && errorData.message) {
                errorMessage = errorData.message;
            }
        } catch {

        }
        throw new Error(errorMessage);
    }

    const dtos: PriceChangeDto[] = await response.json();

    if (dtos.length === 0) {
        throw new Error("No price changes found for this pair");
    }

    const latestDto = dtos[dtos.length - 1];

    return mapPriceChangeDtoToModel(latestDto);
};
