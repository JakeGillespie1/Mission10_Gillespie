import React from "react";
import "./App.css";
import Header from "./Header";
import Footer from "./Footer";
import BowlerList from "./Bowler/BowlerList";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <Header />
        <BowlerList />
        <Footer />
      </header>
    </div>
  );
}

export default App;
