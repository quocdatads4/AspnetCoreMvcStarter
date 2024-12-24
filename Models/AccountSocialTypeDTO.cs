using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcStarter.Models
{
  public class AccountSocialTypeDTO
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // This attribute makes Id auto-increment
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên nhóm không được để trống")]
    public string? Name { get; set; }
  }
}
