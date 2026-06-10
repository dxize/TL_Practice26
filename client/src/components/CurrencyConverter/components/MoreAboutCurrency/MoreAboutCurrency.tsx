import { useState } from "react";
import type { Currency } from "../../../../models";
import { InfoMoreAboutCurrencyConverter } from "./components/InfoMoreAboutCurrencyConverter/InfoMoreAboutCurrencyConverter";
import ArrowSvg from "../../../../assets/ArrowSvg.svg";
import styles from "./MoreAboutCurrency.module.scss";

type MoreAboutCurrencyProps = {
    fromCurrency: Currency;
    toCurrency: Currency;
};

export const MoreAboutCurrency = ({
    fromCurrency,
    toCurrency,
}: MoreAboutCurrencyProps) => {
    const [isOpen, setIsOpen] = useState<boolean>(false);

    return (
        <div className={styles.container}>
            <div className={styles.openAbout}>
                <div className={styles.line} />
                <button
                    className={styles.button}
                    onClick={() => setIsOpen(!isOpen)}
                >
                    <div className={styles.buttonText}>
                        {fromCurrency.code}/{toCurrency.code}: about
                    </div>
                    <img
                        className={
                            isOpen
                                ? `${styles.arrow} ${styles.arrowActive}`
                                : styles.arrow
                        }
                        src={ArrowSvg}
                        alt="Arrow"
                    />
                </button>
                <div className={styles.line} />
            </div>
            {isOpen && (
                <>
                    {fromCurrency && (
                        <InfoMoreAboutCurrencyConverter
                            currencyName={fromCurrency.code}
                            title={`${fromCurrency.name} - ${fromCurrency.code} - ${fromCurrency.symbol}`}
                            description={fromCurrency.description}
                        />
                    )}
                    {toCurrency && (
                        <InfoMoreAboutCurrencyConverter
                            currencyName={toCurrency.code}
                            title={`${toCurrency.name} - ${toCurrency.code} - ${toCurrency.symbol}`}
                            description={toCurrency.description}
                        />
                    )}
                </>
            )}
        </div>
    );
};