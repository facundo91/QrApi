using System;

namespace qrAPI.Dtos
{
    public class PetDto : Dto
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        //public Gender Gender { get; set; }
        //public Person Owner { get; set; }
        public string PictureUrl { get; set; }
    }
}
