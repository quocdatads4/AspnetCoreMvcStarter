using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcStarter.Models
{
  public class ProfileOrbitasDTO
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }

    public DateTime CreatedDate { get; set; }
    public string? Status { get; set; }
    public string? IPAddress { get; set; }
    public string? Country { get; set; }
    public int? ProfileGroupID { get; set; }
    public string? UserId { get; set; }
  }
}
