using System.ComponentModel.DataAnnotations;

namespace vibetogether_backend.DTO
{
    public class RoomCreate
    {
        [Required]
        public string VideoUrl { get; set; }
    }
}
