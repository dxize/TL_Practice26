import { CurrencyConverter } from "../../components/CurrencyConverter/CurrencyConverter";
import { useFetchCurrenciesOnMount } from "./useFetchCurrenciesOnMount";
import styles from "./MainPage.module.scss";

export const MainPage = () => {
    const { isPending, errorMessage, result } = useFetchCurrenciesOnMount();

    if (isPending && (!result || result.length === 0)) {
        return (
            <div className={styles.page}>
                <div className={styles.loader}>Loading...</div>
            </div>
        );
    }

    if (errorMessage) {
        return (
            <div className={styles.page}>
                <div className={styles.errorCard}>
                    COULD NOT GET DATA<br />FROM THE SERVER.
                </div>
            </div>
        );
    }

    return (
        <div className={styles.page}>
            <CurrencyConverter currencies={result ?? []} />
        </div>
    );
};
