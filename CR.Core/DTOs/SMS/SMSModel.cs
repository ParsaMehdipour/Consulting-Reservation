using System.Collections.Generic;

namespace CR.Core.DTOs.SMS
{
    public class SMSModel
    {
        public string op { get; set; } = "pattern";
        public string user { get; set; } = "9118975064";
        public string pass { get; set; } = "rezajuybari73";
        public string fromNum { get; set; } = "+983000505";
        public string toNum { get; set; }
        public string patternCode { get; set; }
        public List<Dictionary<string, string>> inputData { get; set; }
    }
}
