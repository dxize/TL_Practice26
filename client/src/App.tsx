import './App.module.scss'
import {CurrencyConverter} from './components/CurrencyConverter/CurrencyConverter'
import styles from './App.module.scss';

export const App = () => {
  return (
    <div className={styles.app}>
      <CurrencyConverter/>
    </div>
  )
}
