import { useConverter } from "../../hooks/useConverter";
import { currencies, priceChanges } from "../../mocks";
import { HeaderCurrencyConverter } from "./components/HeaderCurrencyConverter/HeaderCurrencyConverter";
import { InputCurrencyCoverter } from "./components/InputCurrencyCoverter/InputCurrencyCoverter";
import { SwapButton } from "./components/SwapButton/SwapButton";
import { MoreAboutCurrency } from "./components/MoreAboutCurrency/MoreAboutCurrency";
import styles from "./CurrencyConverter.module.scss";

export const CurrencyConverter = () => {
    const {
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
    } = useConverter(currencies, priceChanges);

    return (
        <div className={styles.body}>
            <HeaderCurrencyConverter
                fromCurrency={fromCurrency}
                toCurrency={toCurrency}
                priceChange={priceChange}
            />

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
            <MoreAboutCurrency
                key={pairKey}
                fromCurrency={fromCurrency}
                toCurrency={toCurrency}
            />
        </div>
    );
};