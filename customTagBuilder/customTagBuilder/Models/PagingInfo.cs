namespace customTagBuilder.Models
{
    public class PagingInfo
    {
        public int ItemsPerPage { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }

        public int PageCount { get => (int)Math.Ceiling((double)TotalCount / ItemsPerPage); }
    }
}
