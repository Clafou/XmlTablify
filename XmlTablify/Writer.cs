using System;
using System.Collections.Generic;
using System.IO;

namespace XmlTablify
{
    public class Writer
    {
        private List<string> _columnNames;
        private TextWriter _textWriter;
        private int _writtenRowCount;
        private int _writtenRowCountLastCheck;

        public Writer(IEnumerable<string> columnNames, TextWriter textWriter)
        {
            _columnNames = new List<string>(columnNames);
            _textWriter = textWriter;
        }

        public int GetAndResetWrittenRowCount()
        {
            int result = (_writtenRowCount - _writtenRowCountLastCheck);
            _writtenRowCountLastCheck = _writtenRowCount;
            return result;
        }

        public int TotalWrittenRowCount
        {
            get
            {
                return _writtenRowCount;
            }
        }

        public void WriteHeaderRow()
        {
            int lastColumn = _columnNames.Count - 1;
            for (int i = 0; i <= lastColumn; i++)
            {
                string name = _columnNames[i];
                if (i != lastColumn)
                {
                    _textWriter.Write(name);
                    _textWriter.Write("\t");
                }
                else
                {
                    _textWriter.WriteLine(name);
                }
            }
        }

        public void WriteRow(Dictionary<string, string> captures)
        {
            if (_writtenRowCount++ == 0)
            {
                this.WriteHeaderRow();
            }

            int lastColumn = _columnNames.Count - 1;
            for (int i = 0; i <= lastColumn; i++)
            {
                string name = _columnNames[i];
                string value = (captures.ContainsKey(name)) ? captures[name] : String.Empty;
                if (i != lastColumn)
                {
                    _textWriter.Write(value);
                    _textWriter.Write("\t");
                }
                else
                {
                    _textWriter.WriteLine(value);
                }
            }
        }
    }
}

