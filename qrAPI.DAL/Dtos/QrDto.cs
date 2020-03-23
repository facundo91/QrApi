using System.ComponentModel.DataAnnotations.Schema;

namespace qrAPI.DAL.Dtos
{
    [Table("Qrs")]
    public class QrDto : Dto
    {
        public string Name { get; set; }
    }
}