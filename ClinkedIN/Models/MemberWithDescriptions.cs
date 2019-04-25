using System;

namespace ClinkedIn.Models
{
    public class MemberWithDescriptions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Age { get; set; }
        public bool IsPrisoner { get; set; }
    }
}