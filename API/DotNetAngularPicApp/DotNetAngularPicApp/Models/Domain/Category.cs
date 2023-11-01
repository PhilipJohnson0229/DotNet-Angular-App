namespace DotNetAngularPicApp.Models.Domain
{
    public class Category
    {
        public Guid Id
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public String UrlHandle
        {
            get;
            set;
        }

        //set up our many to many relationship
        //a category can have many blog posts and the opposite is also true
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
