import logo from "./Bowling.jpg";

function Header() {
  return (
    <header className="row header navbar navbar-dark bg-dark">
      <div className="col-4">
        <img src={logo} className="logo" alt="logo" width={200} />
      </div>
      <div className="col subtitle">
        <h1 className="text-white">All Bowlers Welcome</h1>
      </div>
    </header>
  );
}

export default Header;
