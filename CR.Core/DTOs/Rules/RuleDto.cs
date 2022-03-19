using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.Rules
{
    public class RuleDto
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "لطفا متن را وارد کنید")]
        public string FullContent { get; set; }

        [Required(ErrorMessage = "لطفا متن را وارد کنید")]
        public string PaymentContent { get; set; }

        [Required(ErrorMessage = "لطفا متن را وارد کنید")]
        public string CommentContent { get; set; }

        [Required(ErrorMessage = "لطفا متن را وارد کنید")]
        public string OtherContent { get; set; }


    }
}
