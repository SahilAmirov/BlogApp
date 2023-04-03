using BlogApp.Models.Entities;

namespace BlogApp.Models.Dtos
{
    public class PostDto
    {
        public Post Post { get; set; }
        public Category Category { get; set; }
        public Writer Writer { get; set; }
    }
}
