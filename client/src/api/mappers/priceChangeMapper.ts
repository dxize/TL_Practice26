import type { PriceChangeDto } from "../dto/PriceChangeDto";
import type { PriceChange } from "../../models";

export const mapPriceChangeDtoToModel = (dto: PriceChangeDto): PriceChange => {
    return {
        fromCurrencyCode: dto.paymentCurrencyCode,
        toCurrencyCode: dto.purchasedCurrencyCode,
        price: dto.price,
        dateTime: dto.dateTime,
    };
};
