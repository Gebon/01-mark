using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Markdown.NET
{
    public class Markdown
    {
        static void Main(string[] args)
        {
        }

        public static string Transform(string text)
        {
            text = StandartiseToUnixLineEndings(text);
            text = StripEmptyLines(text);
            text += "\n\n";
            text = RemoveUnneccessaryNewLines(text);

            text = ProcessParagraphs(text);
            return text;
        }

        private static string ProcessParagraphs(string text)
        {
            var paragraphs = Regex.Split(text, "\n\n")
                .Where(x => !String.IsNullOrWhiteSpace(x))
                .Select(x => Regex.Replace(x, "\n", string.Empty));

            text = "";

            foreach (var paragraph in paragraphs)
            {
                text += String.Format("<p>{0}</p>", paragraph);
            }
            return text;
        }

        private static string RemoveUnneccessaryNewLines(string text)
        {
            return Regex.Replace(text, "\n{3,}", "\n\n");
        }

        private static string StripEmptyLines(string text)
        {
            return Regex.Replace(text, @"^[ \t]+$", string.Empty, RegexOptions.Multiline);
        }

        private static string StandartiseToUnixLineEndings(string text)
        {
            return text.Replace("\r\n", "\n").Replace("\r", "\n");
        }
    }
}
