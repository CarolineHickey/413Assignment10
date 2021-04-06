using System;
using System.Linq;
using BowlingLeague.Models;
using Microsoft.AspNetCore.Mvc;
namespace BowlingLeague.Components
{
    public class TeamViewComponent : ViewComponent
    {

        private BowlingLeagueContext Context;

        public TeamViewComponent(BowlingLeagueContext context)
        {
            Context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(Context.Teams
                .Distinct()
                .OrderBy(x => x));
           
        }
    }
}
