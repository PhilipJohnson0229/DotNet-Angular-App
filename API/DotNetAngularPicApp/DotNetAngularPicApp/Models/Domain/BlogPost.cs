using Microsoft.AspNetCore.Components.Forms;

namespace DotNetAngularPicApp.Models.Domain
{
    public class BlogPost
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

        public DateTime publishedDate
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
    }
}
