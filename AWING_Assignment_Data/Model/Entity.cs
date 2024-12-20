using System.ComponentModel.DataAnnotations;

namespace AWING_Assignment_Data.Model
{
    public class Entity
    {
        [Key]
        public required string ID { get; set; }
    }
}
