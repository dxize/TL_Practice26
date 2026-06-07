import styles from './HeaderCurrencyConverter.module.scss';

export const HeaderCurrencyConverter = () => {
    return (
        <div className={styles.header}>
            <div className={styles.label}>1 Polish zloty is</div>
            <div className={styles.rate}>0.99 Japanese yen</div>
            <div className={styles.date}>Fri, 05 Apr 2026 10:35 UTC</div>
        </div>
    )
}