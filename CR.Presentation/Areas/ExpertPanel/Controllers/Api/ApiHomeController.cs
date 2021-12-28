using CR.Core.DTOs.Experts;
using CR.Core.Services.Interfaces.Experts;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers.Api
{
    [ApiController]
    public class ApiHomeController : ControllerBase
    {
        private readonly IEditExpertDetailsService _editExpertDetailsService;

        public ApiHomeController(IEditExpertDetailsService editExpertDetailsService)
        {
            _editExpertDetailsService = editExpertDetailsService;
        }

    }
}
