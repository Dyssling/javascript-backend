import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

function App() {
  const [email, setEmail] = useState("");

  const handleSubscribe = async (e) => {
    e.preventDefault();

    try{
      const res = await fetch("http://localhost:5000/api/publish", {
        method: "post",
        headers: {
          "content-type": "application/json"
        },
        body: JSON.stringify({payload: email})
      });

      const data = res.json();
    }

    catch{ }
  }

  return (
    <>
        <form onSubmit={handleSubscribe} noValidate>
          <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
          <button type="submit">Submit</button>
        </form>
    </>
  )
}

export default App
