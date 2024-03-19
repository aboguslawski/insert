import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [currencies, setCurrencies] = useState();

    useEffect(() => {
        populateCurrencyData();
    }, []);

    const contents = currencies === undefined
        ? <p><em>Loading...</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Code</th>
                    <th>Description</th>
                    <th>Mid</th>
                </tr>
            </thead>
            <tbody>
                {currencies.map(currency =>
                    <tr key={currency.id}>
                        <td>{currency.code}</td>
                        <td>{currency.description}</td>
                        <td>{currency.mid}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">NBP Currencies</h1>
            {contents}
        </div>
    );
    
    async function populateCurrencyData() {
        const response = await fetch('currency');
        const data = await response.json();
        setCurrencies(data);
    }
}

export default App;