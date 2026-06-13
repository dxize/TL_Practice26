import type { CurrencyDto } from "../dto/CurrencyDto";
import type { Currency } from "../../models";

export const mapCurrencyDtoToModel = (dto: CurrencyDto): Currency => {
    return {
        code: dto.code,
        name: dto.name,
        description: dto.description,
        symbol: dto.symbol,
    };
};
