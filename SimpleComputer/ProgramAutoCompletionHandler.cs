using System;

namespace SimpleComputer
{
    class ProgramAutoCompletionHandler : IAutoCompleteHandler
    {
        public char[] Separators { get; set; } = new char[] { ' ', '/', '\\', '.', };
        private string[] _suggestions;

        public ProgramAutoCompletionHandler(string[] suggestions)
        {
            _suggestions = suggestions;
        }

        public string[] GetSuggestions(string text, int index)
        {
            return _suggestions;
        }
    }
}
