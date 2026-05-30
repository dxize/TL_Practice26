import styles from "./InfoMoreAboutCurrencyConverter.module.scss"

type InfoMoreAboutCurrencyConverterProps = {
    currencyName: string;
};

export const InfoMoreAboutCurrencyConverter = ({currencyName} : InfoMoreAboutCurrencyConverterProps) => {
    
    let title = "";
    let subTitle = ""; 

    if (currencyName === "PLN") {
        title = "Polish zloty - PLN - zł";
        subTitle = "This is the official currency and legal tender of Poland. It is subdivided into 100 grosz-y (gr). It is the most traded currency in Central and Eastern Europe and ranks 21st most-traded in the foreign exchange market. "
    }
    else if (currencyName === "JPY") {
        title = "Japanese yen - JPY - ¥";
        subTitle = "The yen is the official currency of Japan. It is the third-most traded currency in the foreign exchange market, after the United States dollar and the euro. It is also widely used as a third reserve currency after the US dollar and the euro."
    }

    return (
        <div className={styles.container}>
            <div className={styles.containerTitle}>{title}</div>
            <div className={styles.containerSubtitle}>{subTitle}</div>
        </div>
    )
}