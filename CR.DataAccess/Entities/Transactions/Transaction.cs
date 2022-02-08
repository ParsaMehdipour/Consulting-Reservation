using CR.DataAccess.Common.Entity;
using CR.DataAccess.Enums;

namespace CR.DataAccess.Entities.Transactions
{
    public class Transaction : BaseEntity
    {
        public long PayerId { get; set; }
        public long ReceiverId { get; set; }
        public long Price_Digit { get; set; }
        public string Price_String { get; set; }
        public string CreateDate_String { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionStatus Status { get; set; }

        #region Navigation Properties

        public long? FactorId { get; set; }

        #endregion
    }
}
