namespace ProductApplication.Models.Entity
{
    public class PaginatedCategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalProducts { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalProducts / PageSize);
    }
}
