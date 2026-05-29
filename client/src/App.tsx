import './App.module.scss'
import CurrencyConverter from './components/CurrencyConverter/CurrencyConverter'
import styles from './App.module.scss';

function App() {
  return (
    <div className={styles.app}>
      <CurrencyConverter/>
    </div>
  )
}

export default App
