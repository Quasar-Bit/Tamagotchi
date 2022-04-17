namespace Tamagotchi.Web.Services.DTOs.OutPut.Common
{
    public class Photo
    {
        public string small { get; set; }
        public string medium { get; set; }
        public string large { get; set; }
        public string full { get; set; }
    }

    public class Address
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
    }

    public class Pagination
    {
        public int count_per_page { get; set; }
        public int total_count { get; set; }
        public int current_page { get; set; }
        public int total_pages { get; set; }
        public Links _links { get; set; }
    }

    public class Href
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Href self { get; set; }
        public Href type { get; set; }
        public Href organization { get; set; }
        public Href next { get; set; }
        public Href animals { get; set; }
        public Href breeds { get; set; }
    }
}