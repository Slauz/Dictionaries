using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionaries
{
    [Serializable]
    internal class Word
    {
        public string Word_ { get; private set; }
        public List<string> Translation { get; private set; }
        public Word(string Word_, List<string> Translation)
        {
            this.Word_ = Word_;
            this.Translation = Translation;
        }
        public Word(string Word_)
        {
            this.Word_ = Word_;
            this.Translation = new List<string>();
        }
        public void DeleteTranslation(string ToDelete)
        {
            for(int i = 0; i < Translation.Count; i++) 
            {
                if (Translation[i] == ToDelete)
                {
                    Translation.RemoveAt(i);
                    break;
                }
            }
        }
        public void RewriteWordTranslation()
        {
            Console.WriteLine("Обновить слово или его перевод:( ← - слово;  → - перевод)");

            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.LeftArrow)
            {
                Console.WriteLine("Введите обновленное слово: ");
                string new_word = Console.ReadLine();

                if (new_word == null)
                {
                    return;
                }

                Word_ = new_word;
                return;

               /* for (int i = 0; i < Words.Count; i++)
                {
                    if (library.Dictionaries[DictionaryID].Words[i].Word_ == this_word)
                    {
                        library.Dictionaries[DictionaryID].Words[i].Word_ = new_word;
                        break;
                    }
                }*/
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                Console.WriteLine("Введите перевод который хотите поменять: ");
                string this_translate = Console.ReadLine();

                Console.WriteLine("Введите новый перевод: ");
                string new_translate = Console.ReadLine();

                if (new_translate == null || this_translate == null)
                {
                    return;
                }

                for (int i = 0; i < Translation.Count; i++)
                {
                    if (Translation[i] == this_translate)
                    {
                        Translation[i] = new_translate;
                        break;
                    }
                } 
            }
        }
  
    }
}
