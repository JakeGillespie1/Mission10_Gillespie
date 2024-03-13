import { useState, useEffect } from "react";
import { Bowler } from "../types/Bowler";

function BowlerList() {
  //Left Side: we have the state object and the state setter
  //Right Side: Create an MarriottFood array of objects that defines the type of the useState (Marriott food is passed in as an array that is initially blank)
  const [bowlerData, setBowlerData] = useState<Bowler[]>([]);

  //If it does not need to get the data (because nothing has changed, then we wont constantly be getting the data)
  useEffect(() => {
    //Set up thigs that are going to happen and then call the fatch food data to bring it in.
    const fetchBowlerData = async () => {
      const rsp = await fetch("http://localhost:5197/bowlerinformation");
      const f = await rsp.json();
      setBowlerData(f);
    };
    fetchBowlerData();
    //If we do not find anything, just pass in an empty array
  }, []);

  return (
    <>
      <div className="row">
        <h4 className="text-center">Here is a list of our current bowlers!</h4>
      </div>
      <table className="table table-bordered">
        <thead>
          <tr>
            <th>Full Name</th>
            <th>Team Name</th>
            <th>Address</th>
            <th>City</th>
            <th>State</th>
            <th>Zip</th>
            <th>Phone Number</th>
          </tr>
        </thead>
        <tbody>
          {/* Get our food data and map it. Our initial variable is f that keeps track of which piece of data we are on */}
          {bowlerData.map((f) => (
            <tr key={f.bowlerId}>
              <td>{f.bowlerName}</td>
              <td>{f.bowlerTeam}</td>
              <td>{f.bowlerAddress}</td>
              <td>{f.bowlerCity}</td>
              <td>{f.bowlerState}</td>
              <td>{f.bowlerZip}</td>
              <td>{f.bowlerPhone}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  );
}

export default BowlerList;
