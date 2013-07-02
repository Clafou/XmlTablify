using System;
using System.Text.RegularExpressions;

namespace XmlTablify
{
    public class Column
    {
        public string Name { get; private set; }

        public string Select { get; private set; }

        public Regex Regex { get; private set; }

        public Column(Config.Column column)
        {
            Name = column.Name;
            Select = column.Select;
            if (!String.IsNullOrEmpty(column.Match))
            {
                Regex = new Regex(column.Match, RegexOptions.Compiled);
                if (Regex.GetGroupNumbers().Length != 2)
                {
                    throw new Exception("Invalid regular expression pattern: " + column.Match + "\n The expression must contain exactly one group (in parentheses)");
                }
            }
        }

        public string Capture(string text)
        {
            if (Regex != null)
            {
                Match match = Regex.Match(text);
                return (match.Success) ? match.Groups[1].Value : null;
            }
            return text;
        }
    }
}

