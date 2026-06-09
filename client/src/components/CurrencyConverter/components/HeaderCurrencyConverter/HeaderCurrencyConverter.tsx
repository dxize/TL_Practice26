import type { Currency, PriceChange } from "../../../../models";
import styles from './HeaderCurrencyConverter.module.scss';

type HeaderCurrencyConverterProps = {
    fromCurrency: Currency;
    toCurrency: Currency;
    priceChange: PriceChange | null;
};

export const HeaderCurrencyConverter = ({
    fromCurrency,
    toCurrency,
    priceChange,
}: HeaderCurrencyConverterProps) => {
    const formattedDate = priceChange
        ? new Date(priceChange.dateTime).toUTCString()
        : "";

    return (
        <div className={styles.header}>
            <div className={styles.label}>1 {fromCurrency.name} is</div>
            <div className={styles.rate}>
                {priceChange ? priceChange.price : "—"} {toCurrency.name}
            </div>
            {formattedDate && (
                <div className={styles.date}>{formattedDate}</div>
            )}
        </div>
    )
}