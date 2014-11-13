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
        public static string Transform(string text)
        {
            text = ToUnixLineEndings(text);
            text = StripEmptyLines(text);
            text = RemoveSingeNewLines(text);
            text += "\n\n";
            text = RemoveUnneccessaryNewLines(text);

            var headers = Regex.Matches(text, "#+ *(?<header>[^#\n]+)#");

            text = StrongTag(text);
            text = EmTags(text);

            text = ProcessParagraphs(text);
            return text;
        }

        private static string StrongTag(string text)
        {
            return Regex.Replace(text, @"(?<strong>__[^_{2}]+__)", StrongReplacer);
        }

        private static string StrongReplacer(Match match)
        {
            return Replace(match, "__", "strong");
        }

        private static string Replace(Match match, string s, string groupName)
        {
            var tag = String.Format("<{0}>", groupName);
            var tmp = match.Groups[groupName].Value.Replace(s, tag);
            tmp = tmp.Insert(tmp.LastIndexOf(tag, StringComparison.Ordinal) + 1, "/");
            return match.Value.Replace(match.Groups[groupName].Value, tmp);
        }

        private static string EmTags(string text)
        {
            return Regex.Replace(text, @"(?<em>_[^_]+_)", EmReplacer, RegexOptions.Multiline);
        }

        private static string EmReplacer(Match match)
        {
            return Replace(match, "_", "em");
        }

        private static string RemoveSingeNewLines(string text)
        {
            return Regex.Replace(text, @"[^\n](\n)[^\n]", SingleNewLinesReplacer, RegexOptions.Multiline);
        }

        private static string SingleNewLinesReplacer(Match match)
        {
            return match.Value.Replace("\n", " ");
        }

        private static string ProcessParagraphs(string text)
        {
            var paragraphs = Regex.Split(text, "\n\n")
                .Where(x => !String.IsNullOrWhiteSpace(x));

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

        private static string ToUnixLineEndings(string text)
        {
            return text.Replace("\r\n", "\n").Replace("\r", "\n");
        }
    }
}
