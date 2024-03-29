﻿using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Http;
using System;

namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestEditConsumerDetailsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthDate_String { get; set; }
        public string BloodType { get; set; }
        public string SpecificAddress { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string degree { get; set; }
        public string IconSrc { get; set; }
        public IFormFile IconImage { get; set; }
        public long ConsumerInformationId { get; set; }
        public long ConsumerId { get; set; }
        public string Email { get; set; }
        public GenderType gender { get; set; }
    }
}
