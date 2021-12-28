using CR.DataAccess.Enums;

namespace CR.Core.DTOs.ResultDTOs
{
    public class ResultCheckUserFlagService
    {
        public UserFlag UserFlag { get; set; }
        public bool IsActive { get; set; }
    }
}
