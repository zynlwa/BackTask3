namespace BackendProject.App.ViewModels
{
    public class BookTestVm
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPercentage { get; set; }
        public bool IsFeatured { get; set; } 
        public bool IsNew { get; set; }
        public bool InStock { get; set; }
        public string Code { get; set; }
        public string MainImageUrl { get; set; }
        public string HoverImageUrl { get; set; }
        public string AuthorName { get; set; }
        public string GenreName { get; set; }
        public List<string> BookImageUrls { get; set; }
        public List<string> TagNames { get; set; }



    }
}
