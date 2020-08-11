using System;

namespace PortfolioWebApp.Models
{
    public class Work
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public DateTime Login { get; set; }
        public DateTime Logout { get; set; }

    }
}