using System.ComponentModel;

namespace BookStoreMVC.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [DefaultValue(0)]
        public int ParentId { get; set; }
        public string Name { get; set; }
    }
}