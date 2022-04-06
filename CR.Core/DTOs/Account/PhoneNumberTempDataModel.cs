using System;

namespace CR.Core.DTOs.Account
{
    public class PhoneNumberTempDataModel
    {
        public string PhoneNumber { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
