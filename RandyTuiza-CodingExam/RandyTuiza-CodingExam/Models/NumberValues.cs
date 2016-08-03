using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandyTuiza_CodingExam.Models
{
    public class Predictive
    {
        public static Dictionary<int, string> keyMaps = new Dictionary<int, string>()
        {
            {0,"" },
            {1,"" },
            {2,"ABC" },
            {3,"DEF" },
            {4,"GHI" },
            {5,"JKL" },
            {6,"MNO" },
            {7,"PQRS" },
            {8,"TUV" },
            {9,"WXYZ" }
        };

        public static Dictionary<char, int> letterMaps;

        static Predictive()
        {
            letterMaps = keyMaps
            .SelectMany(m => m.Value.Select(c => new { ch = c, num = m.Key }))
            .ToDictionary(x => x.ch, x => x.num);
        }

        List<string> words = new List<string>();

        Predictive[] edges = new Predictive[10];

        public IEnumerable<string> Words
        {
            get { return words; }
        }

        public void Add(string word, int pos = 0)
        {
            if (pos == word.Length)
            {
                if (word.Length > 0)
                {
                    words.Add(word);
                }
                return;
            }
            var currentChar = word[pos];
            int edgeIndex = 0;
            try
            {
                edgeIndex = letterMaps[currentChar];
            }
            catch (Exception) { }
            if (edges[edgeIndex] == null)
            {
                edges[edgeIndex] = new Predictive();
            }
            var nextPredic = edges[edgeIndex];
            nextPredic.Add(word, pos + 1);
        }

        public Predictive FindMostPopulatedNode()
        {
            Stack<Predictive> stk = new Stack<Predictive>();
            stk.Push(this);
            Predictive biggest = null;
            while (stk.Any())
            {
                var node = stk.Pop();
                biggest = biggest == null
                   ? node
                   : (node.words.Count > biggest.words.Count
                       ? node
                       : biggest);
                foreach (var next in node.edges.Where(e => e != null))
                {
                    stk.Push(next);
                }
            }
            return biggest;
        }

        public IEnumerable<string> Search(string numberSequenceString)
        {
            var numberSequence = numberSequenceString
                                   .Select(n => int.Parse(n.ToString()));
            return Search(numberSequence);
        }

        private IEnumerable<string> Search(IEnumerable<int> numberSequence)
        {
            if (!numberSequence.Any())
            {
                return words;
            }
            var first = numberSequence.First();
            var remaining = numberSequence.Skip(1);
            var nextPredic = edges[first];
            if (nextPredic == null)
            {
                return Enumerable.Empty<string>();
            }
            return nextPredic.Search(remaining);
        }
    }
}