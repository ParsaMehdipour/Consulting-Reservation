using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Http;

namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestAddConsumerDetailsDto
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string birthDate_String { get; set; }

        public string bloodType { get; set; }

        public string specificAddress { get; set; }

        public string province { get; set; }

        public string city { get; set; }

        public string degree { get; set; }

        public string phoneNumber { get; set; }

        public IFormFile iconImage { get; set; }

        public long consumerId { get; set; }

        public string email { get; set; }
        public GenderType gender { get; set; }
    }
}
