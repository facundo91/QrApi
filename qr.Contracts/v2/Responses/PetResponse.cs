﻿using System;
using qrAPI.Contracts.v2.Requests;

namespace qrAPI.Contracts.v2.Responses
{
    public class PetResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public Guid Owner { get; set; }
        public string PictureUrl { get; set; }
    }
}
