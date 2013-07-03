using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Linq;

namespace XmlTablify.Config
{
    public class Configuration : IDisposable
    {
        private List<IDisposable> _disposables;

        public List<Job> Jobs { get; private set; }

        public int PrintStatusNodeCount { get; private set; }

        public Configuration(string path)
        {
            _disposables = new List<IDisposable>();
            Jobs = new List<Job>();

            XmlSerializer deserializer = new XmlSerializer(typeof(Transform));
            using (TextReader reader = new StreamReader(path))
            {
                Transform transform = (Transform)deserializer.Deserialize(reader);
                PrintStatusNodeCount = (transform.PrintStatusNodeCount > 0) ? transform.PrintStatusNodeCount : 10000;

                foreach (Table table in transform.tables)
                {
                    TextWriter textWriter;
                    if (String.IsNullOrEmpty(table.Output))
                    {
                        textWriter = Console.Out;
                    }
                    else
                    {
                        textWriter = new StreamWriter(table.Output);
                        _disposables.Add(textWriter);
                    }

                    Dictionary<string, List<XmlTablify.Column>> capturePathToColumns = new Dictionary<string, List<XmlTablify.Column>>();
                    foreach (var column in table.Columns)
                    {
                        if (!capturePathToColumns.ContainsKey(column.Select))
                        {
                            capturePathToColumns[column.Select] = new List<XmlTablify.Column>();
                        }
                        capturePathToColumns[column.Select].Add(new XmlTablify.Column(column));
                    }

                    Jobs.Add(new Job(table.Output,
                                     table.RowSelect,
                                     capturePathToColumns,
                                     textWriter));
                }
            }
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}

