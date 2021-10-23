namespace Frontend.Models
{
    public class AdvancedSearchModel
    {
        public string SearchValueEmail { get; set; }
        public string SearchValueFirstName { get; set; }
        public string SearchValueLastName { get; set; }
        public string SearchValuePhoneNumber { get; set; }
        public string SearchValuePosition { get; set; }
        public int OrderBy { get; set; }
        public bool OrderDesc { get; set; }
        public bool Paged { get; set; }
    }
}