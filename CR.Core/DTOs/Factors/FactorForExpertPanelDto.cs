﻿namespace CR.Core.DTOs.Factors
{
    public class FactorForExpertPanelDto
    {
        public string FactorNumber { get; set; }
        public long ConsumerId { get; set; }
        public string ConsumerIconSrc { get; set; }
        public string ConsumerFullName { get; set; }
        public string TotalPrice { get; set; }
        public string CreateDate { get; set; }
        public string Status { get; set; }
    }
}
