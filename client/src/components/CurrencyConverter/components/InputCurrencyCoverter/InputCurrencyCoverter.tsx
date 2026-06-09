import type { Currency } from "../../../../models";
import styles from "./InputCurrencyCoverter.module.scss"
import TriangleSvg from "../../../../assets/TriangleSvg.svg"

type InputCurrencyCoverterProps = {
    value: string;
    onValueChange?: (value: string) => void;
    currencies: Currency[];
    selectedCode: string;
    onCurrencyChange: (code: string) => void;
    readOnly?: boolean;
};

export const InputCurrencyCoverter = ({
    value,
    onValueChange,
    currencies,
    selectedCode,
    onCurrencyChange,
    readOnly = false,
}: InputCurrencyCoverterProps) => {

    const inputOnChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const input = e.target.value;

        if (/^\d*(,\d*)?$/.test(input) && onValueChange) {
            onValueChange(input);
        }
    }

    const inputOnBlur = () => {
        if (value === "" && onValueChange) {
            onValueChange("1");
        }
    }

    const handleSelectChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        onCurrencyChange(e.target.value);
    };

    return (
        <div className={styles.container}>
            <input
                className={styles.input}
                type="text"
                value={value}
                onChange={inputOnChange}
                onBlur={inputOnBlur}
                readOnly={readOnly}
            />
            <div className={styles.separator} />
            <div className={styles.selectWrapper}>
                <select
                    className={styles.select}
                    value={selectedCode}
                    onChange={handleSelectChange}
                >
                    {currencies.map((currency) => (
                        <option key={currency.code} value={currency.code}>
                            {currency.code}
                        </option>
                    ))}
                </select>
                <img className={styles.icon} src={TriangleSvg} alt="" />
            </div>
        </div>
    )
}