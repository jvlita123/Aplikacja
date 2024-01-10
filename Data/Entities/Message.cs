using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Entities;

public partial class Message 
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public int UserId { get; set; }

    public int UserId2 { get; set; }

    public Boolean IsNew { get; set; } = false;

    [ForeignKey("UserId")]
    public virtual User User1 { get; set; } = null!;
   
    [NotMapped]
  //  [ForeignKey("UserId2")]
    public virtual User User2 { get; set; } = null!;
}