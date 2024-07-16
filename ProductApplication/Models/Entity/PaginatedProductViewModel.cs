namespace ProductApplication.Models.Entity
{
    public class PaginatedProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalProducts { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalProducts / PageSize);
    }
}
