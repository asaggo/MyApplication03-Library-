using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyApplication03_Library_.Models
{
    public class Book
    {
        [Key]
        public int book_num { get; set; }
        [DisplayName("Title")]
        public string title { get; set; }
        [DisplayName("Writer")]
        public string writer { get; set; }
        [DisplayName("Summary")]
        public string summary { get; set; }
        [DisplayName("Publisher")]
        public string publisher { get; set; }
        [DisplayName("Published Date")]
        public int published_date { get; set; }
        //public DateTime published_date2 { get; set; }
    }
}