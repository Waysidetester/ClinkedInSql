using System;

namespace ClinkedIN.Models
{
    public class MatchedUserService
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public string UserName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Age { get; set; }
        public bool IsPrisoner { get; set; }
    }
}