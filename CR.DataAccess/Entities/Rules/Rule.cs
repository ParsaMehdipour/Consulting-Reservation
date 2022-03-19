using CR.DataAccess.Common.Entity;

namespace CR.DataAccess.Entities.Rules
{
    public class Rule : BaseEntity
    {
        public string FullContent { get; set; }
        public string PaymentContent { get; set; }
        public string CommentContent { get; set; }
        public string OtherContent { get; set; }
    }
}
