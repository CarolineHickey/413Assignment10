using System;
namespace BowlingLeague.Models.ViewModels
{
    public class PageNumberingInfo
    {
        public int NumOfItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public string CurrentTeam { get; set; }

        public int TotalNumItems { get; set; }

        //Calculate the Number of Pages
        public int NumPages => (int) Math.Ceiling((decimal)TotalNumItems / NumOfItemsPerPage);

    }
}
