using System.ComponentModel.DataAnnotations;

namespace GazelServices.Models;

public class OrderRequest
{
    [Required, StringLength(60)]
    public string FromCity { get; set; } = "";

    [Required, StringLength(60)]
    public string ToCity { get; set; } = "";

    [Required, Range(0.1, 1_000_000)]
    public decimal Weight { get; set; }

    [Required, Phone, StringLength(30)]
    public string Phone { get; set; } = "";
}
