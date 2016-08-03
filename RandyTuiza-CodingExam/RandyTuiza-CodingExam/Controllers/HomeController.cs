using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RandyTuiza_CodingExam.Models;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Net;

namespace RandyTuiza_CodingExam.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            List<ResultModel> words = new List<ResultModel>();

            return View(words);
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            List<ResultModel> model = new List<ResultModel>();
            Predictive predict = new Predictive();
            LoadPrediction(predict);
            var searchTerm = collection["combination"];
            var wordList = predict.Search(searchTerm);
            foreach (var item in wordList)
            {
                ResultModel rm = new ResultModel();
                rm.Search = Convert.ToInt32(searchTerm);
                rm.Words = item;
                model.Add(rm);
            }

            return View(model);
        }

        private void LoadPrediction(Predictive root)
        {
            string url = "http://www-01.sil.org/linguistics/wordlists/english/wordlist/wordsEn.txt";
            string stringResult = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            WebResponse response = request.GetResponse();
            using (Stream respStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                stringResult = reader.ReadToEnd();

                string[] result = Regex.Split(stringResult, "\r\n");

                reader.Dispose();
                reader.Close();
                request.Abort();

                foreach (string item in result)
                {
                    root.Add(item.ToString().ToUpper());
                }
            }
        }

        public ActionResult test()
        {
            return View();
        }
    }
}
