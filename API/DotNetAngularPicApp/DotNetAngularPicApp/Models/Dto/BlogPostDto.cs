namespace DotNetAngularPicApp.Models.Dto
{
    public class BlogPostDto
    {
        public Guid Id
        {
            get;
            set;
        }

        public String Title
        {
            get;
            set;
        }

        public String ShortDescription
        {
            get;
            set;
        }

        public String Content
        {
            get;
            set;
        }

        public String FeaturedImageUrl
        {
            get;
            set;
        }

        public String UrlHandle
        {
            get;
            set;
        }

        public DateTime PublishedDate
        {
            get;
            set;
        }

        public String Author
        {
            get;
            set;
        }

        public bool IsVisible
        {
            get;
            set;
        }

        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}
