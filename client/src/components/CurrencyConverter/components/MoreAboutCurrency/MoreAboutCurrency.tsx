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
            <div className={styles.containerOpenAbout}>
                <div className={styles.containerOpenAboutLine}/>
                <button className={styles.containerOpenAboutButton} onClick={() => setIsOpen(!isOpen)}>
                    <div className={styles.containerOpenAboutButtonText}>{fromCurrency}/{toCurrency}: about</div>
                    <img className={isOpen ? `${styles.containerOpenAboutButtonArrow} ${styles.containerOpenAboutButtonArrowActive}` : styles.containerOpenAboutButtonArrow} src={ArrowSvg}/>
                </button>
                <div className={styles.containerOpenAboutLine}/>
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