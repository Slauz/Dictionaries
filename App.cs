using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dictionaries
{
    internal class App
    {
        private Library library { get; set; }

        private delegate void MenuHandler();

        private int pointer = 0;

        private MenuHandler menuHandler;

        public App() 
        { 
            library = new Library();
        }    
        public void Run()
        {
            //WriteOutFromFile();
            menuHandler = Menu;
            while(true)
            {
                Console.Clear();
                menuHandler();
                // WriteInFile();
            }
        }

        private void Menu()
        {   
            Console.WriteLine("Добро пожаловать в \"Словари\"\n");

            Console.WriteLine("Используйте ↑ ↓ для навигации.");

            string[] lines = new string[2];
            lines[0] = " [1] Словари.";
            lines[1] = " [2] Добавить словарь.";
            lines[2] = " [3] Выйти.";

            for (int i = 0; i < lines.Length; i++) 
            { 
                if(i == pointer)
                {
                    Console.WriteLine("=>" + lines[i]);
                    continue;
                }
                Console.WriteLine(lines[i]);
            }

            ConsoleKeyInfo key = Console.ReadKey();

            if(key.Key == ConsoleKey.UpArrow)
            {
                if(pointer > 0)
                {
                    pointer--;
                }
                else
                {
                    return;
                }
            }
            else if(key.Key == ConsoleKey.DownArrow)
            {
                if (pointer < lines.Length-1)
                {
                    pointer++;
                }
                else
                {
                    return;
                }
            }
            else if(key.Key == ConsoleKey.Enter)
            {
                switch(pointer)
                {
                   case 0:
                        {
                            pointer = 0;
                            menuHandler = DictionaryList;
                            break;
                        }
                   case 1: 
                        {
                            pointer = 0;
                            menuHandler = AddDictionary;
                            break;
                        }
                   case 2:
                        {
                            pointer = -1;
                            break;
                        } 
                }
            }
        }
        private void DictionaryList()
        {
            Console.WriteLine("Список словарей.\n");

            Console.WriteLine("Используйте ↑ ↓ для навигации.");

            string[] lines = new string[2+library.Dictionaries.Count];
            lines[0] = " [1] Назад.";
            lines[1] = " [2] Найти словарь.";

            if (library.Dictionaries.Count > 0)
            {
                for (int i = 2; i < library.Dictionaries.Count + 2; i++)
                {
                    lines[i] = library.Dictionaries[i-2].Language1 + " " + library.Dictionaries[i-2].Language2;
                }
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (i == pointer)
                {
                    Console.WriteLine("=>" + lines[i]);
                    continue;
                }
                Console.WriteLine(lines[i]);
            }

            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.UpArrow)
            {
                if (pointer > 0)
                {
                    pointer--;
                }
                else
                {
                    return;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                if (pointer < lines.Length-1)
                {
                    pointer++;
                }
                else
                {
                    return;
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                if (pointer == 0)
                {
                    pointer = 0;
                    menuHandler = Menu;
                }
                else if(pointer == 1)
                {
                    pointer = 0;
                    menuHandler = SearchDictionary;
                }
                else if(pointer >= 2)
                {
                    library.DictionaryID = pointer - 2;
                    pointer = 0;
                    menuHandler = OpenDictionary;
                }
            }
        }
        private void AddDictionary()
        {
            Console.WriteLine("Добавление словаря.\n");

            Console.WriteLine("Используйте ↑ ↓ для навигации.");

            string[] lines = new string[2];
            lines[0] = " [1] Назад.";
            lines[1] = " [2] Ввести языки словаря.";

            for (int i = 0; i < lines.Length; i++)
            {
                if (i == pointer)
                {
                    Console.WriteLine("=>" + lines[i]);
                    continue;
                }
                Console.WriteLine(lines[i]);
            }

            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.UpArrow)
            {
                if (pointer > 0)
                {
                    pointer--;
                }
                else
                {
                    return;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                if (pointer < lines.Length)
                {
                    pointer++;
                }
                else
                {
                    return;
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                switch (pointer)
                {
                    case 0:
                        {
                            pointer = 0;
                            menuHandler = Menu;
                            break;
                        }
                    case 1:
                        {
                            pointer = 0;
                            menuHandler = InputDictionary; 
                            break;
                        }
                }
            }
        }
        private void InputDictionary()
        {
            Console.WriteLine("Оформление словаря.\n");

            Dictionary temp_dict = new Dictionary();

            library.Add(temp_dict);
            menuHandler = AddDictionary;
        }
        private void OpenDictionary()
        {
            Dictionary current_dictionary = library.Dictionaries[library.DictionaryID];
            Console.WriteLine($"{current_dictionary.Language1}-{current_dictionary.Language2} словарь.\n");

            int extra_lines = 0;

            if(current_dictionary.Words.Count > 0)
            {
                extra_lines = current_dictionary.Words.Count;
            }

            string[] lines = new string[3 + extra_lines];
            lines[0] = " [1] Назад.";
            lines[1] = " [2] Найти слово.";
            lines[2] = " [3] Добавить слово.";

            if (current_dictionary.Words.Count > 0)
            {
                for (int i = 3; i < current_dictionary.Words.Count + 3; i++)
                {
                    lines[i] = current_dictionary.Words[i - 3].Word_ + " ";

                    for(int j = 0; j < current_dictionary.Words[i-3].Translation.Count; j++)
                    {
                        lines[i] += current_dictionary.Words[i - 3].Translation[j] + " ";
                    }
                }
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (i == pointer)
                {
                    Console.WriteLine("=>" + lines[i]);
                    continue;
                }
                Console.WriteLine(lines[i]);
            }

            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.UpArrow)
            {
                if (pointer > 0)
                {
                    pointer--;
                }
                else
                {
                    return;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                if (pointer < lines.Length-1)
                {
                    pointer++;
                }
                else
                {
                    return;
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                if (pointer == 0)
                {
                    library.DictionaryID = 0;
                    pointer = 0;
                    menuHandler = DictionaryList;
                }
                else if (pointer == 1)
                {
                    pointer = 0;
                    menuHandler = SearchWord;
                }
                else if (pointer == 2)
                {
                    pointer = 0;
                    menuHandler = AddWord;
                }
                else if (pointer >= 3)
                {
                    pointer = 0;
                    menuHandler = OpenWord;
                }
            }
        }
        private void OpenWord()
        {
            Word current_word = library.Dictionaries[library.DictionaryID].Words[library.Dictionaries[library.DictionaryID].WordID];

            Console.Write($"{current_word.Word_}-");
            
            for(int i = 0; i < current_word.Translation.Count; i++) 
            {
                Console.Write($"{current_word.Translation[i]}; ");
            }
            Console.WriteLine();

            string[] lines = new string[3];
            lines[0] = " [1] Назад.";
            lines[1] = " [2] Обновить слово/перевод.";
            lines[2] = " [3] Удалить слово/перевод.";

            for (int i = 0; i < lines.Length; i++)
            {
                if (i == pointer)
                {
                    Console.WriteLine("=>" + lines[i]);
                    continue;
                }
                Console.WriteLine(lines[i]);
            }

            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.UpArrow)
            {
                if (pointer > 0)
                {
                    pointer--;
                }
                else
                {
                    return;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                if (pointer < lines.Length)
                {
                    pointer++;
                }
                else
                {
                    return;
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                switch (pointer)
                {
                    case 0:
                        {
                            pointer = 0;
                            menuHandler = OpenDictionary;
                            break;
                        }
                    case 1:
                        {
                            library.Dictionaries[library.DictionaryID].WordID = pointer-1;
                            pointer = 0;
                            menuHandler = RewriteWordTranslation;
                            break;
                        }
                    case 2:
                        {
                            library.Dictionaries[library.DictionaryID].WordID = pointer-2;
                            pointer = 0;
                            menuHandler = RemoveWordTranslation;
                            break;
                        }
                }
            }
        }
        private void AddWord()
        {
            Console.WriteLine("Добавление слова.\n");

            library.Dictionaries[library.DictionaryID].AddWord();

            menuHandler = OpenDictionary;
        }       
        public void RewriteWordTranslation()
        {
            Console.WriteLine("Обновление слова/перевода.");

            Word this_word = library.Dictionaries[library.DictionaryID].Words[library.Dictionaries[library.DictionaryID].WordID];

            if (this_word == null)
            {
                return;
            }

            this_word.RewriteWordTranslation();
            menuHandler = OpenWord;
        }
        public void RemoveWordTranslation()
        {
            Console.WriteLine("Удаление слова/перевода.");

            Word this_word = library.Dictionaries[library.DictionaryID].Words[library.Dictionaries[library.DictionaryID].WordID];

            if (this_word == null)
            {
                return;
            }

            library.Dictionaries[library.DictionaryID].RemoveWordTranslation(this_word);

            menuHandler = OpenDictionary;
        }
        public void SearchWord()
        {
            Console.WriteLine("Поиск слова.\n");

            library.Dictionaries[library.DictionaryID].SearchWord();

            Console.WriteLine("Нажмите Esc для возврата в меню.");

            ConsoleKeyInfo key = Console.ReadKey();

            pointer = 0;

            menuHandler = OpenDictionary;
        }
        public void SearchDictionary()
        {
            Console.WriteLine("Поиск словаря.\n");

            int id = library.FindDictionary();

            if(id == -1)
            {
                menuHandler = DictionaryList;
                return;
            }

            library.DictionaryID = id;
            menuHandler = OpenDictionary;
        }
        private void WriteInFile()
        {
            string fileName = "Library.json";
            string serialized_obj = JsonSerializer.Serialize(library);
            File.WriteAllText(fileName, serialized_obj);
        }
        private void WriteOutFromFile()
        {
                string fileName = "Library.json";
                string jsonData = File.ReadAllText(fileName);
                library = JsonSerializer.Deserialize<Library>(jsonData);

                if (library == null)
                {
                    return;
                }
        }
    }
}
