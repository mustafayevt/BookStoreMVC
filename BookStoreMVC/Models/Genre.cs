using System.ComponentModel;

namespace BookStoreMVC.Models
{
    public class Genre
    {
        public Genre(string name)
        {
            Name = name;
        }

        public Genre(int parentId, string name)
        {
            ParentId = parentId;
            Name = name;
        }

        public Genre()
        {
            
        }
        public int Id { get; set; }
        [DefaultValue(0)]
        public int ParentId { get; set; }
        public string Name { get; set; }
    }
}