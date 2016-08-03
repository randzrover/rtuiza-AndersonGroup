using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RandyTuiza_CodingExam.Models;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Text;

namespace RandyTuiza_CodingExam.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public List<string> Get(int id)
        {
            List<string> wordsResult = new List<string>();
            Predictive predict = new Predictive();
            LoadPrediction(predict);
            var searchTerm = id.ToString();
            var wordList = predict.Search(searchTerm);
            foreach (var item in wordList)
            {
                wordsResult.Add(item);
            }

            return wordsResult;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
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
    }
}
