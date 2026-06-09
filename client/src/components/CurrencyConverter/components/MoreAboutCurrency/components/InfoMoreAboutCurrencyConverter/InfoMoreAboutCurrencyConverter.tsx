import styles from "./InfoMoreAboutCurrencyConverter.module.scss";

type InfoMoreAboutCurrencyConverterProps = {
    currencyName: string;
    title: string;
    description: string;
};

const FALLBACK_TEXT = "No description available.";

export const InfoMoreAboutCurrencyConverter = ({
    currencyName,
    title,
    description,
}: InfoMoreAboutCurrencyConverterProps) => {
    return (
        <div
            className={styles.container}
            data-testid={`currency-info-${currencyName}`}
        >
            <div className={styles.title}>{title}</div>
            <div className={styles.subtitle}>
                {description || FALLBACK_TEXT}
            </div>
        </div>
    );
};