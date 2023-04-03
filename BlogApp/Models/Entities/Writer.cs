namespace BlogApp.Models.Entities
{
    public class Writer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
