using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.Appointments;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Enums;
using System.Collections.Generic;
using CR.DataAccess.Entities.FinancialTransactions;

namespace CR.DataAccess.Entities.Factors
{
    public class Factor : BaseEntity
    {
        public string FactorNumber { get; set; }
        public FactorStatus FactorStatus { get; set; }
        public string CardHolderPAN { get; set; }
        public string RefId { get; set; }
        public long SaleReferenceId { get; set; }
        public long TotalPrice { get; set; }

        #region ForeignKeys

        public long? ExpertInformationId { get; set; }
        public long? ConsumerInformationId { get; set; }

        #endregion

        #region NavigationProperties

        public virtual ExpertInformation ExpertInformation { get; set; }
        public virtual ConsumerInfromation ConsumerInformation { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<FinancialTransaction> FinancialTransactions { get; set; }

        #endregion
    }
}
