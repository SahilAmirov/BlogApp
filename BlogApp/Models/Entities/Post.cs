
namespace BlogApp.Models.Entities
{
    public class Post
    {
        public int ID { get; set; }
        public string Title  { get; set; }
        public string Details  { get; set; }
        public string PhotoLink  { get; set; }
        public DateTime PublishDate { get; set; }

        
        public Writer Writer { get; set; }
        public int WriterID { get; set; }

        public Category Category { get; set; }
        public int CategoryID { get; set; }
        
    }
}
