namespace Frontend.Models
{
    public class SimpleSearchModel
    {
        public string SearchValue { get; set; }
        public int OrderBy { get; set; }
        public bool OrderDesc { get; set; }
        public bool Paged { get; set; }
    }
}