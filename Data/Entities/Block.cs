using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Entities;

public partial class Block
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BlockedUserId { get; set; }

    [NotMapped]
    public virtual User BlockedUser { get; set; } = null!;
    [NotMapped]
    public virtual User User { get; set; } = null!;
}
