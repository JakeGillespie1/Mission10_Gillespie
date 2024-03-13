using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Mission10_GillespieAPI.Data
{
    public class EFBowlingRepository : IBowlingRepository
    {

        private BowlingLeagueContext _context;

        /*Constructor that pulls in an instance of the database to use*/
        public EFBowlingRepository(BowlingLeagueContext temp) { 
            _context = temp;
        }

        /*IEnumerable will iterate over the Bowler List*/
        public IEnumerable<Bowler> Bowlers => _context.Bowlers;

        public IEnumerable<Team> Teams => _context.Teams;
    }
}
