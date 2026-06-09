import { useState, useMemo } from "react";
import type { Currency, PriceChange } from "../models";

export const useConverter = (
    currencies: Currency[],
    priceChanges: Record<string, Record<string, PriceChange>>
) => {
    const [fromCode, setFromCode] = useState(currencies[0].code);
    const [toCode, setToCode] = useState(currencies[1].code);
    const [amount, setAmount] = useState("1");

    const fromCurrency = useMemo(
        () => currencies.find((c) => c.code === fromCode)!,
        [currencies, fromCode]
    );

    const toCurrency = useMemo(
        () => currencies.find((c) => c.code === toCode)!,
        [currencies, toCode]
    );

    const priceChange = priceChanges[fromCode]?.[toCode] ?? null;

    const result = useMemo(() => {
        const numericAmount = parseFloat(amount.replace(",", "."));

        if (isNaN(numericAmount) || !priceChange) {
            return "";
        }

        return (numericAmount * priceChange.price).toFixed(2);
    }, [amount, priceChange]);

    /**
     * При совпадении выбранной валюты с другим селектом —
     * автоматически переключает второй селект на первую
     * доступную отличающуюся валюту.
     */
    const findAlternative = (excludeCode: string): string => {
        const alt = currencies.find((c) => c.code !== excludeCode);
        return alt ? alt.code : excludeCode;
    };

    const handleFromChange = (code: string) => {
        setFromCode(code);

        if (code === toCode) {
            setToCode(findAlternative(code));
        }
    };

    const handleToChange = (code: string) => {
        setToCode(code);

        if (code === fromCode) {
            setFromCode(findAlternative(code));
        }
    };

    const swap = () => {
        setFromCode(toCode);
        setToCode(fromCode);
    };

    /** Используется как key для дочерних компонентов, чтобы сбросить их state при смене пары */
    const pairKey = `${fromCode}-${toCode}`;

    return {
        fromCode,
        toCode,
        amount,
        result,
        pairKey,
        fromCurrency,
        toCurrency,
        priceChange,
        setAmount,
        handleFromChange,
        handleToChange,
        swap,
    };
};
