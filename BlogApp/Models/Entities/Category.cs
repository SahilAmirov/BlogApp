
namespace BlogApp.Models.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
