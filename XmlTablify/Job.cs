using System;
using System.Collections.Generic;
using System.IO;

namespace XmlTablify
{
    public class Job
    {
        private string _rowScope;
        private Dictionary<string, string> _capturePathToName;

        public String Name { get; private set; }
        public Writer Writer { get; private set; }

        public Job(string name, string rowScope, Dictionary<string, string> capturePathToName, TextWriter textWriter)
        {
            _rowScope = rowScope;
            _capturePathToName = capturePathToName;
            Name = name;
            Writer = new Writer(this.ColumnNames, textWriter);
        }

        public IEnumerable<string> ColumnNames
        {
            get
            {
                return _capturePathToName.Values;
            }
        }

        public bool IsRowScope(string path)
        {
            return _rowScope.Equals(path);
        }

        public String GetCaptureName(string path)
        {
            if (_capturePathToName.ContainsKey(path))
            {
                return _capturePathToName[path];
            }
            return null;
        }
    }
}

