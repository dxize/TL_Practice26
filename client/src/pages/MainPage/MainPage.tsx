import { CurrencyConverter } from "../../components/CurrencyConverter/CurrencyConverter";
import styles from "./MainPage.module.scss";

export const MainPage = () => {
    return (
        <div className={styles.page}>
            <CurrencyConverter />
        </div>
    );
};
