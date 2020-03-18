using System.ComponentModel.DataAnnotations.Schema;

namespace qrAPI.Dtos
{
    [Table("Qrs")]
    public class QrDto : Dto
    {
        public string Name { get; set; }
    }
}