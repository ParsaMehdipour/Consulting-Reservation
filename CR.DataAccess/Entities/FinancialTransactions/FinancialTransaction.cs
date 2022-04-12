using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.Factors;
using CR.DataAccess.Enums;

namespace CR.DataAccess.Entities.FinancialTransactions
{
    public class FinancialTransaction : BaseEntity
    {
        public long PayerId { get; set; }
        public long ReceiverId { get; set; }
        public long Price_Digit { get; set; }
        public string Price_String { get; set; }
        public string CreateDate_String { get; set; }
        public string TransactionNumber { get; set; }
        public string RefId { get; set; }
        public string CardHolderPAN { get; set; }
        public long SaleReferenceId { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionStatus Status { get; set; }
        public string Description { get; set; }

        #region Foreign Keys

        public long? FactorId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Factor Factor { get; set; }

        #endregion
    }
}
