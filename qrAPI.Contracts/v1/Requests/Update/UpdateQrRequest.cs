using System.ComponentModel.DataAnnotations;

namespace qrAPI.Contracts.v1.Requests.Update
{
    public class UpdateQrRequest
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
    }
}