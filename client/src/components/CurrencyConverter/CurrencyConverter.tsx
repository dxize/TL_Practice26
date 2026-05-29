import HeaderCurrencyConverter from "./components/HeaderCurrencyConverter/HeaderCurrencyConverter";
import InputCurrencyCoverter from "./components/InputCurrencyCoverter/InputCurrencyCoverter";
import MoreAboutCurrency from "./components/MoreAboutCurrency/MoreAboutCurrency";
import styles from "./CurrencyConverter.module.scss";

function CurrencyConverter() {
    return (
        <div className={styles.currencyConverterBody}>
            <HeaderCurrencyConverter />
            <InputCurrencyCoverter currencyName="PLN" currencyRate="1" />
            <InputCurrencyCoverter currencyName="JPY" currencyRate="0,99" />
            <MoreAboutCurrency/>
        </div>
    );
}

export default CurrencyConverter;