using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcStarter.Models
{
  public class ProfileOrbitasDTO
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? ProfileName { get; set; }
    public string? Status { get; set; }
    public int? ProfileGroupID { get; set; }
    public string? UserId { get; set; }
  }
}
