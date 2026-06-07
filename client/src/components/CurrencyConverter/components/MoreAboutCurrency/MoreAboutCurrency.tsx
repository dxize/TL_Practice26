import { useState } from "react";
import styles from "./MoreAboutCurrency.module.scss"
import {InfoMoreAboutCurrencyConverter} from "./components/InfoMoreAboutCurrencyConverter/InfoMoreAboutCurrencyConverter";
import ArrowSvg from "../../../../assets/ArrowSvg.svg"

type MoreAboutCurrencyProps = {
    fromCurrency: string;
    toCurrency: string;
};

export const MoreAboutCurrency = ({ fromCurrency, toCurrency }: MoreAboutCurrencyProps) => {
    const [isOpen, setIsOpen] = useState<boolean>(false);

    return (
        <div className={styles.container}>
            <div className={styles.openAbout}>
                <div className={styles.line}/>
                <button className={styles.button} onClick={() => setIsOpen(!isOpen)}>
                    <div className={styles.buttonText}>{fromCurrency}/{toCurrency}: about</div>
                    <img className={isOpen ? `${styles.arrow} ${styles.arrowActive}` : styles.arrow} src={ArrowSvg}/>
                </button>
                <div className={styles.line}/>
            </div>
            {isOpen && (
                <>
                    <InfoMoreAboutCurrencyConverter currencyName={fromCurrency}/>
                    <InfoMoreAboutCurrencyConverter currencyName={toCurrency}/>
                </>
            )}
        </div>
    )
}