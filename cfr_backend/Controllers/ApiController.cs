using CFRDal;
using CFRDal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Web.Mvc;

namespace cfr_backend.Controllers
{
    public class ApiController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IManager _dal;
        public ApiController(IManager dal)
        {
            _dal = dal;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.JsonResult GetMovie(int id)
        {
            string output = _dal.GetMovie(id);
            Console.Out.WriteLine(output);

            var encoderSettings = new TextEncoderSettings();
            encoderSettings.AllowCharacters('\u0022');
            encoderSettings.AllowRange(UnicodeRanges.BasicLatin);

            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            /*return new HttpResponseMessage()
            {
                Content = new StringContent(output, System.Text.Encoding.UTF8, "application/json")
            };*/

            return Json(output, options);
        }
    }
}
