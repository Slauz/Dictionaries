namespace Dictionaries
{
    internal class Program
    {
        enum MenuItems
        {
            Menu = 1,
            DictionaryList,
            AddDictionary,
            AddWord,
            AddTranslation,
            SearchDictionary,
            SearchWord,
            RefreshWord,
            Back
        }

        static void Main(string[] args)
        {
            App app = new App();
            app.Run();
        }
    }
}