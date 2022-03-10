using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.Rules
{
    public class RuleDto
    {
        public long Id { get; set; }

        [Required]
        public string FullContent { get; set; }

    }
}
