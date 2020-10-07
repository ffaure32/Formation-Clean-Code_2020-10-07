using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Questions
    {
        private readonly Dictionary<string, Queue<string>> categories;

        public Questions()
        {
            categories = new Dictionary<string, Queue<string>>();
            categories["Pop"] = new Queue<string>();
            categories["Science"] = new Queue<string>();
            categories["Sports"] = new Queue<string>();
            categories["Rock"] = new Queue<string>();
        }
        public void AddQuestion(string category, string question)
        {
            categories[category].Enqueue(question);
        }

        public string NextQuestion(string category)
        {
            var linkedList = categories[category];
            return linkedList.Dequeue();
        }
    }
}