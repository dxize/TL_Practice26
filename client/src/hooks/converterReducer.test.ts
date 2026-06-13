import { expect, test, describe } from 'vitest';
import { converterReducer, initialState, type ConverterState } from './useConverter';
import type { Currency } from '../models';

describe('converterReducer', () => {
    const mockCurrencies: Currency[] = [
        { code: 'USD', name: 'US Dollar', description: '', symbol: '$' },
        { code: 'EUR', name: 'Euro', description: '', symbol: '€' }
    ];

    test('transitions to loading state on FETCH_CURRENCIES_START', () => {
        const state: ConverterState = { ...initialState, isLoading: false, error: 'some error' };
        
        const nextState = converterReducer(state, { type: 'FETCH_CURRENCIES_START' });

        expect(nextState.isLoading).toBe(true);
        expect(nextState.error).toBe(null);
    });

    test('saves data correctly on FETCH_CURRENCIES_SUCCESS', () => {
        const nextState = converterReducer(initialState, { 
            type: 'FETCH_CURRENCIES_SUCCESS', 
            payload: mockCurrencies 
        });

        expect(nextState.isLoading).toBe(false);
        expect(nextState.currencies).toEqual(mockCurrencies);
        expect(nextState.fromCode).toBe('USD');
        expect(nextState.toCode).toBe('EUR');
    });

    test('saves error correctly on FETCH_CURRENCIES_ERROR', () => {
        const state: ConverterState = { ...initialState, isLoading: true };
        
        const nextState = converterReducer(state, { 
            type: 'FETCH_CURRENCIES_ERROR', 
            payload: 'Network Error' 
        });

        expect(nextState.isLoading).toBe(false);
        expect(nextState.error).toBe('Network Error');
    });
});
