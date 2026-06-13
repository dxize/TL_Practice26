import { useConverter } from "../../hooks/useConverter";

import { HeaderCurrencyConverter } from "./components/HeaderCurrencyConverter/HeaderCurrencyConverter";
import { InputCurrencyCoverter } from "./components/InputCurrencyCoverter/InputCurrencyCoverter";
import { SwapButton } from "./components/SwapButton/SwapButton";
import { MoreAboutCurrency } from "./components/MoreAboutCurrency/MoreAboutCurrency";
import styles from "./CurrencyConverter.module.scss";

export const CurrencyConverter = () => {
    const {
        state: { amount, fromCode, toCode, currencies, isLoading, error, toastError, priceChange },
        result,
        pairKey,
        fromCurrency,
        toCurrency,
        setAmount,
        handleFromChange,
        handleToChange,
        swap,
    } = useConverter();

    if (isLoading && currencies.length === 0) {
        return <div className={styles.loader}>Loading...</div>;
    }

    if (error) {
        return (
            <div className={styles.errorCard}>
                COULD NOT GET DATA<br />FROM THE SERVER.
            </div>
        );
    }

    return (
        <div className={styles.body}>
            {toastError && <div className={styles.toast}>{toastError}</div>}
            
            {fromCurrency && toCurrency && (
                <HeaderCurrencyConverter
                    fromCurrency={fromCurrency}
                    toCurrency={toCurrency}
                    priceChange={priceChange}
                />
            )}

            <div className={styles.converterRow}>
                <SwapButton onClick={swap} />

                <div className={styles.inputsColumn}>
                    <InputCurrencyCoverter
                        value={amount}
                        onValueChange={setAmount}
                        currencies={currencies}
                        selectedCode={fromCode}
                        onCurrencyChange={handleFromChange}
                    />

                    <InputCurrencyCoverter
                        value={result}
                        currencies={currencies}
                        selectedCode={toCode}
                        onCurrencyChange={handleToChange}
                        readOnly
                    />
                </div>
            </div>

            {/*
              key={pairKey} — при смене валютной пары React размонтирует
              и смонтирует компонент заново, что сбрасывает его внутреннее
              состояние (isOpen → false). Это проще и надёжнее, чем
              пробрасывать управление открытием наружу через пропсы,
              потому что состояние open/closed — деталь реализации
              MoreAboutCurrency, и родитель не должен о ней знать.
              Так же если делать через пропсы произойдёт перерендер всех 
              компонентов, что может вызвать лишние вычисления.
            */}
            {fromCurrency && toCurrency && (
                <MoreAboutCurrency
                    key={pairKey}
                    fromCurrency={fromCurrency}
                    toCurrency={toCurrency}
                />
            )}
        </div>
    );
};