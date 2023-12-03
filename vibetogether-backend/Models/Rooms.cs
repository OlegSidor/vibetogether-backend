using System.ComponentModel.DataAnnotations;

namespace vibetogether_backend.Models
{
    public class Rooms
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string VideoURL { get; set; }
        public Guid? UserId { get; set; }
    }
}
