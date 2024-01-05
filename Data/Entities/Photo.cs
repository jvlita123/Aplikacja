using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Data.Entities;

public partial class Photo
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Description { get; set; }

    public string? Path { get; set; }

    public bool? IsProfilePicture { get; set; }

    public DateTime? Date { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}
