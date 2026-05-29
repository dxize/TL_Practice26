import { useState } from "react";
import styles from "./InputCurrencyCoverter.module.scss"

type InputCurrencyCoverterProps = {
    currencyName: string;
    currencyRate: string;
};

function InputCurrencyCoverter({currencyName, currencyRate,}: InputCurrencyCoverterProps) {

    const [value, setValue] = useState<string>(currencyRate);

    const InputOnChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const input = e.target.value;

        if (/^\d*(,\d*)?$/.test(input)) {
            setValue(input);
        }
    }
    
    const inputOnBlur = () => {
        if (value === "") {
            setValue("1");
        }
    }

    return (
        <div className={styles.container}>
            <input className={styles.containerInput} type="text" value={value} onChange={InputOnChange} onBlur={inputOnBlur}/>
            <div className={styles.containerSeparator}/>
            <div className={styles.containerCurrencyName}>{currencyName}</div>
            <button className={styles.containerButton}>
                <svg viewBox="0 0 26 23" fill="none" xmlns="http://www.w3.org/2000/svg"> 
                    <path d="M12.9902 22.5L-0.000146866 0L25.9806 0L12.9902 22.5Z" fill="#D9D9D9"/>
                </svg>
            </button>
        </div>
    )
}

export default InputCurrencyCoverter;