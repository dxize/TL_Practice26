import { useConverter } from "../../hooks/useConverter";

import { HeaderCurrencyConverter } from "./components/HeaderCurrencyConverter/HeaderCurrencyConverter";
import { InputCurrencyCoverter } from "./components/InputCurrencyCoverter/InputCurrencyCoverter";
import { SwapButton } from "./components/SwapButton/SwapButton";
import { RateChart } from "./components/RateChart/RateChart";
import { MoreAboutCurrency } from "./components/MoreAboutCurrency/MoreAboutCurrency";
import styles from "./CurrencyConverter.module.scss";

export const CurrencyConverter = () => {
    const {
        state: { amount, fromCode, toCode, currencies, isLoading, error },
        result,
        pairKey,
        fromCurrency,
        toCurrency,
        setAmount,
        handleFromChange,
        handleToChange,
        swap,
        latestPrice,
        chartData,
        chartLoading,
        chartError,
        periodMinutes,
        setPeriodMinutes,
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

            {fromCurrency && toCurrency && (
                <HeaderCurrencyConverter
                    fromCurrency={fromCurrency}
                    toCurrency={toCurrency}
                    priceChange={latestPrice}
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

                <div className={styles.chartColumn}>
                    <RateChart
                        chartData={chartData}
                        chartLoading={chartLoading}
                        chartError={chartError}
                        periodMinutes={periodMinutes}
                        onPeriodChange={setPeriodMinutes}
                    />
                </div>
            </div>

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