using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

namespace CR.Presentation.Controllers.View
{
    public class ReportsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;
        public ReportsController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetReport()
        {
            StiReport report = new StiReport();
            var file = Path.Combine(_environment.WebRootPath, "Reports/ChaleChoole.mrt");
            report.Load(file);
            report["CompanyName"] = "سامانه یکپارچه چاله چوله";
            object myObject = GetReportSnapshot();
            report.RegBusinessObject("FactorModel", myObject);
            report.Load(file);
            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }

        public static FactorModel GetReportSnapshot()
        {

            var factorModel = new FactorModel()
            {
                BirthDate = "1379/04/23",
                Address = "بابلسر - کوچه شهید امام - پلاک 22",
                FullName = "پارسا مهدی پور",
                Gender = "مرد",
                CreateDate = "1400/11/02",
                FactorNumber = "123456789",
                PostalCode = "777444669922"
            };

            return factorModel;

        }

    }



    public class FactorModel
    {
        public string FactorNumber { get; set; }

        public string CreateDate { get; set; }

        public string FullName { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public string BirthDate { get; set; }

        public string PostalCode { get; set; }

    }
}

