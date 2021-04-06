using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BowlingLeague.Models;
using Microsoft.EntityFrameworkCore;
using BowlingLeague.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //access to the database
        private BowlingLeagueContext Context { get; set; }

        // a variable that lets me know what team im on
        public string TeamCategory { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext context)
        {
            _logger = logger;
            Context = context;
        }

        public IActionResult Index(int? teamid, string teamName, int pageNum = 0)
        {
            int numOfPages = 5;

            return View(new IndexViewModel
            {
                //Setting the bowlers information
                Bowlers = Context.Bowlers
                .Where(x => x.TeamId == teamid || teamid == null)
                .OrderBy(x => x.Team)
                .Skip((pageNum - 1) * numOfPages)
                .Take(numOfPages)
                .ToList(),

                //if no team has been selected, go get the count of everything. If there is a team
                //then go get the count of the players
                PageNumberingInfo = new PageNumberingInfo
                {
                    NumOfItemsPerPage = numOfPages,
                    CurrentPage = pageNum,
                    TotalNumItems = (teamid == null ? Context.Bowlers.Count() :
                    Context.Bowlers.Where(x => x.TeamId == teamid).Count())
                },

                TeamCategory = teamName
            });
        }
            //Context.Bowlers
                //.FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId = {teamid} OR {teamid} IS NULL")

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
