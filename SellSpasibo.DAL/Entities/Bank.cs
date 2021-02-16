using System.ComponentModel.DataAnnotations;

namespace SellSpasibo.DAL.Entities
{
    public class Bank
    {
        [Key]
        public int Id { get; set; }
        public string MemberId { get; set; }
        public string Name { get; set; }
    }
}
