using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission10_GillespieAPI.Data;

namespace Mission10_GillespieAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BowlerInformationController : ControllerBase
    {
        private IBowlingRepository _bowlingRepository;

        /*Constructor that will pull in the data gathered by the interfaces requests to the repo*/
        public BowlerInformationController(IBowlingRepository temp)
        {
            _bowlingRepository = temp;
        }

        [HttpGet]
        public IEnumerable<object> Get()
        {
            //Join the tables together to get the needed information
            /*
            Line-by-line
            Line 1 (_context.Bowlers): Access the Bowlers table from the _context object. This is the source of data for the query.
            Lines 2-5:
                3. (bowler => bowler.TeamId): Specifies the key selector function for the 'Bowlers' table, extracting the TeamId property from each Bowler object (row)
                4. (team => team.TeamId): Specifies the key selector function for the Teams table extracting the TeamId Property from each Team object (row)
                5. ((bowler, team) => new { Bowler = bowler, Team = team })): specifies the result selector function, creating a new anonymous object with properties Bowler and Team representing the joined rows
            Line 6 (.Where...): Keeps the rows where the team name is either of the specified string items.
            Line 7 (.Select...): This projects each item of the anonymous type back to the object. Now, Query will contain a list of anonymous type objects, 
            Line 7 (return bowlerData): The results (anonymous type object) are returned and casted to type 'object'
             */
            var bowlerData = _bowlingRepository.Bowlers.AsEnumerable()
                        .Join(_bowlingRepository.Teams,
                               bowler => bowler.TeamId,
                               team => team.TeamId,
                               (bowler, team) => new
                               {
                                   Bowler = bowler,
                                   Team = team
                               })
                        .Where(bowlerAndTeam => bowlerAndTeam.Team.TeamName == "Marlins" || bowlerAndTeam.Team.TeamName == "Sharks")
                        .Select(bowlerAndTeam => new
                        {
                            bowlerId = bowlerAndTeam.Bowler.BowlerId,
                            bowlerName = $"{bowlerAndTeam.Bowler.BowlerFirstName} {bowlerAndTeam.Bowler.BowlerMiddleInit} {bowlerAndTeam.Bowler.BowlerLastName}",
                            bowlerTeam = bowlerAndTeam.Team.TeamName,
                            bowlerAddress = bowlerAndTeam.Bowler.BowlerAddress,
                            bowlerCity = bowlerAndTeam.Bowler.BowlerCity,
                            bowlerState = bowlerAndTeam.Bowler.BowlerState,
                            bowlerZip = bowlerAndTeam.Bowler.BowlerZip,
                            bowlerPhone = bowlerAndTeam.Bowler.BowlerPhoneNumber
                        }).ToArray();

            return bowlerData;
        }

    }
}
