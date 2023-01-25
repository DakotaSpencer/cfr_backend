using CFRDal;
using CFRDal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
            var movie = _dal.GetMovie(id);
            return Json(movie);

            /*
            string output = _dal.GetMovie(id);
            Console.Out.WriteLine(output);

            var encoderSettings = new TextEncoderSettings();
            encoderSettings.AllowRange(UnicodeRanges.BasicLatin);

            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };*/

            //output = output.Replace('"', '\'');

            /*return new HttpResponseMessage()
            {
                Content = new StringContent(output, System.Text.Encoding.UTF8, "application/json")
            };*/

            //var result = JObject.Parse(output);

            //return JsonSerializer.Serialize(new Utf8JsonWriter(), output);
        }
    }
}
