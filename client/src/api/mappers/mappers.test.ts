import { expect, test, describe } from 'vitest';
import { mapCurrencyDtoToModel } from './currencyMapper';
import { mapPriceChangeDtoToModel } from './priceChangeMapper';
import type { CurrencyDto } from '../dto/CurrencyDto';
import type { PriceChangeDto } from '../dto/PriceChangeDto';

describe('Mappers', () => {
    test('mapCurrencyDtoToModel correctly maps DTO to Model', () => {
        const dto: CurrencyDto = {
            code: 'USD',
            name: 'US Dollar',
            description: 'United States Dollar',
            symbol: '$',
        };

        const result = mapCurrencyDtoToModel(dto);

        expect(result).toEqual({
            code: 'USD',
            name: 'US Dollar',
            description: 'United States Dollar',
            symbol: '$',
        });
    });

    test('mapPriceChangeDtoToModel correctly maps DTO to Model', () => {
        const dto: PriceChangeDto = {
            purchasedCurrencyCode: 'JPY',
            paymentCurrencyCode: 'CAD',
            price: 0.741,
            dateTime: '2026-05-21T03:40:54.2709677Z',
        };

        const result = mapPriceChangeDtoToModel(dto);

        expect(result).toEqual({
            toCurrencyCode: 'JPY',
            fromCurrencyCode: 'CAD',
            price: 0.741,
            dateTime: '2026-05-21T03:40:54.2709677Z',
        });
    });
});
