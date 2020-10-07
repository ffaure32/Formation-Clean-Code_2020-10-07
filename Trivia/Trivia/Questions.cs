using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public enum Category
    {
        Pop = 0,
        Science = 1,
        Sports =2,
        Rock = 3
    }
    public class Questions
    {
        private readonly Dictionary<Category, Queue<string>> categories;

        public Questions()
        {
            categories = new Dictionary<Category, Queue<string>>();
            foreach (Category category in Enum.GetValues(typeof(Category)))
            {
                categories[category] = new Queue<string>();
            }
        }
        public void AddQuestion(Category category, string question)
        {
            categories[category].Enqueue(question);
        }

        public string NextQuestion(Category category)
        {
            var linkedList = categories[category];
            return linkedList.Dequeue();
        }
    }
}