using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XmlTablify
{
    public class Job
    {
        private string _rowScope;
        private Dictionary<string, List<Column>> _capturePathToColumn;
        private readonly IList<Column> _noColumns = new List<Column>().AsReadOnly();

        public String Name { get; private set; }
        public Writer Writer { get; private set; }

        public Job(string name, string rowScope, Dictionary<string, List<Column>> capturePathToColumns, TextWriter textWriter)
        {
            _rowScope = rowScope;
            _capturePathToColumn = capturePathToColumns;

            Name = name;
            List<string> columnNames = capturePathToColumns.SelectMany(x => x.Value.Select(y => y.Name)).ToList();
            Writer = new Writer(columnNames, textWriter);
        }

        public bool IsRowScope(string path)
        {
            return _rowScope.Equals(path);
        }

        public IList<Column> GetCaptureColumns(string path)
        {
            if (_capturePathToColumn.ContainsKey(path))
            {
                return _capturePathToColumn[path];
            }
            return _noColumns;
        }
    }
}

