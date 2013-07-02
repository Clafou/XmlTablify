using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XmlTablify
{
    public class Job
    {
        private string _rowScope;
        private Dictionary<string, Column> _capturePathToColumn;

        public String Name { get; private set; }
        public Writer Writer { get; private set; }

        public Job(string name, string rowScope, Dictionary<string, Column> capturePathToColumn, TextWriter textWriter)
        {
            _rowScope = rowScope;
            _capturePathToColumn = capturePathToColumn;

            Name = name;
            List<string> columnNames = capturePathToColumn.Select(x => x.Value.Name).ToList();
            Writer = new Writer(columnNames, textWriter);
        }

        public bool IsRowScope(string path)
        {
            return _rowScope.Equals(path);
        }

        public Column GetCaptureColumn(string path)
        {
            if (_capturePathToColumn.ContainsKey(path))
            {
                return _capturePathToColumn[path];
            }
            return null;
        }
    }
}

