using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Entities.Users;

namespace CR.DataAccess.Entities.CommissionAndDiscounts
{
    public class CommissionAndDiscount : BaseEntity
    {
        public double PhoneCallCommission { get; set; }
        public double VoiceCallCommission { get; set; }
        public double TextCallCommission { get; set; }
        public double PhoneCallDiscount { get; set; }
        public double VoiceCallDiscount { get; set; }
        public double TextCallDiscount { get; set; }

        #region Foreing Keys

        public long ExpertInformationId { get; set; }

        #endregion

        #region Navigation Properties

        public ExpertInformation ExpertInformation { get; set; }

        #endregion
    }
}
