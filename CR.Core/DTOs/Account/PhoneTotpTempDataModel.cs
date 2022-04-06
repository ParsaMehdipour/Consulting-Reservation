using System;

namespace CR.Core.DTOs.Account
{
    public class PhoneTempDataModel
    {
        public byte[] SecretKey { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
