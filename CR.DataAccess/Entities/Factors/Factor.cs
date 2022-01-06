using System.Collections.Generic;
using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.Appointments;
using CR.DataAccess.Enums;

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

        #region NavigationProperties

        public List<Appointment> Appointments { get; set; }

        #endregion
    }
}
