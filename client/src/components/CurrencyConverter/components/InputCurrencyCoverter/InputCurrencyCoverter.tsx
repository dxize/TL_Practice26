import { useState } from "react";
import styles from "./InputCurrencyCoverter.module.scss"
import TriangleSvg from "../../../../assets/TriangleSvg.svg"

type InputCurrencyCoverterProps = {
    currencyName: string;
    currencyRate: string;
};

export const InputCurrencyCoverter = ({currencyName, currencyRate}: InputCurrencyCoverterProps) => {

    const [value, setValue] = useState<string>(currencyRate);
    const [isOpen, setIsOpen] = useState<boolean>(false);

    const inputOnChange = (e: React.ChangeEvent<HTMLInputElement>) => {
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
            <input className={styles.input} type="text" value={value} onChange={inputOnChange} onBlur={inputOnBlur}/>
            <div className={styles.separator}/>
            <button className={styles.button} onClick={() => setIsOpen(!isOpen)}>
                <div className={styles.currencyName}>{currencyName}</div>
                <div className={isOpen ? `${styles.icon} ${styles.iconActive}` : styles.icon}>
                    <img src={TriangleSvg}/>
                </div>
            </button>
        </div>
    )
}