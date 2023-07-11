using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Dictionaries
{
    [Serializable]
    internal class Dictionary
    {
        public List<Word> Words { get; private set; }
        public string Language1 { get; private set; }
        public string Language2 { get; private set; }

        public int WordID = 0;
        public Dictionary()
        {
            Console.WriteLine("Введите первый язык словаря:");
            Language1 = Console.ReadLine();
            Console.WriteLine("Введите второй язык словаря:");
            Language2 = Console.ReadLine();

            Words = new List<Word>();
        }
        public Dictionary(string language1, string language2)
        {
            Words = new List<Word>();
            Language1 = language1;
            Language2 = language2;
        }
        public void RemoveWordTranslation(Word w)
        {
            Console.WriteLine("Удалить слово или его перевод:( ← - слово;  → - перевод)");

            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.LeftArrow)
            {
                for (int i = 0; i < Words.Count; i++)
                {
                    if (Words[i] == w)
                    {
                        Words.RemoveAt(i);
                        break;
                    }
                }
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                Console.WriteLine("Введите перевод который хотите удалить: ");
                string this_translate = Console.ReadLine();

                if (this_translate == null)
                {
                    return;
                }

                int word_id = 0;

                for (int i = 0; i < Words.Count; i++)
                {
                    if (Words[i] == w)
                    {
                        word_id = i;
                        break;
                    }
                }

                for (int i = 0; i < Words[word_id].Translation.Count; i++)
                {
                    if (Words[word_id].Translation[i] == this_translate)
                    {
                        Words[word_id].Translation.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        public void SearchWord()
        {
            Console.WriteLine("Введите слово, перевод которого ищете:");

            string word = Console.ReadLine();

            for (int i = 0; i < Words.Count; i++)
            {
                if (Words[i].Word_ == word)
                {
                    Console.WriteLine($"{Words[i].Word_} - ");
                    for (int j = 0; j < Words[i].Translation.Count; j++)
                    {
                        Console.Write(Words[i].Translation[j] + " ");
                    }
                    Console.WriteLine();
                    break;
                }
            }
        }
        public void AddWord()
        {
            Console.WriteLine("Введите слово:");
            string word = Console.ReadLine();
            List<string> translates = AddTranslations();

            if (word == null || translates == null || translates.Count == 0)
            {
                return;
            }

            Word wt = new Word(word, translates);
            Words.Add(wt);
        }
        public List<string> AddTranslations()
        {
            List<string> translations = new List<string>();

            Console.WriteLine("Введите перевод слова:");

            string translation = Console.ReadLine();

            if (translation == null)
            {
                return translations;
            }

            translations.Add(translation);

            Console.WriteLine("Хотите добавить еще перевод?( ← - нет; → - да;)");

            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.LeftArrow)
            {
                return translations;
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                List<string> temp_l = AddTranslations();
                for (int i = 0; i < temp_l.Count; i++)
                {
                    translations.Add(temp_l[i]);
                }
            }
            return translations;
        }
    }
}

