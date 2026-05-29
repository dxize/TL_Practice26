import styles from './HeaderCurrencyConverter.module.scss';

function HeaderCurrencyConverter() {
    return (
        <div className={styles.header}>
            <div className={styles.headerLabel}>1 Polish zloty is</div>
            <div className={styles.headerRate}>0.99 Japanese yen</div>
            <div className={styles.headerDate}>Fri, 05 Apr 2026 10:35 UTC</div>
        </div>
    )
}

export default HeaderCurrencyConverter;