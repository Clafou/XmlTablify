using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlTablify.Config
{
    public class Transform
    {
        [XmlElement("Table")]
        public List<Table> tables = new List<Table>();

        [XmlAttribute("PrintStatusNodeCount")]
        public int PrintStatusNodeCount { get; set; }
    }

    public class Table
    {
        [XmlAttribute("Output")]
        public string Output { get; set; }

        [XmlAttribute("RowSelect")]
        public string RowSelect { get; set; }

        [XmlElement("Column")]
        public List<Column> Columns { get; set; }
    }

    public class Column
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Select")]
        public string Select { get; set; }

        [XmlAttribute("Match")]
        public string Match { get; set; }
    }
}

