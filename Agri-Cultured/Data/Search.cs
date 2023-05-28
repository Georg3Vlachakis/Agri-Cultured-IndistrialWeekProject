namespace Agri_Cultured.Data
{
    public class Search
    {
        public Search() { }

        public Search(string date, string description)
        {
            Date = date;
            Description = description;
        }

        public string Date { get; set; }
        public string Description { get; set; }
    }
}
