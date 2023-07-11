using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dictionaries
{
    [Serializable]
    internal class Library
    {
        public List<Dictionary> Dictionaries { get; set; }

        public int DictionaryID = 0;
        public Library()
        {
            Dictionaries = new List<Dictionary>();
        }
        public void Add(Dictionary dictionary)
        {
            Dictionaries.Add(dictionary);
        }

        public int FindDictionary()
        {
            Console.WriteLine("Введите первый язык словаря:");
            string temp_lang1 = Console.ReadLine();
            Console.WriteLine("Введите второй язык словаря:");
            string temp_lang2 = Console.ReadLine();

            if (temp_lang1 == null || temp_lang2 == null)
            {
                return -1;
            }

            for (int i = 0; i < Dictionaries.Count; i++)
            {
                if (temp_lang1.ToLower() == Dictionaries[i].Language1.ToLower() && temp_lang2.ToLower() == Dictionaries[i].Language2.ToLower())
                {
                    return i;
                }
            }

            return -1;
        }
 
    }
}
