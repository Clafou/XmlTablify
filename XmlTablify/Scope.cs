using System;
using System.Collections.Generic;

namespace XmlTablify
{
    public class Scope
    {
        private string _currentPath;
        private bool _inRow;
        private string _text;
        private Job _job;
        private Scope _parent;
        private Dictionary<string, string> _captures;

        public Scope(Job job)
        {
            _job = job;
            _captures = new Dictionary<string, string>();
        }

        public Scope(Scope parent, string tag)
        {
            _parent = parent;
            _job = parent._job;
            _currentPath = parent._currentPath + "/" + tag;
            _inRow = parent._inRow || _job.IsRowScope(_currentPath);
            _captures = (parent._inRow) ? parent._captures : new Dictionary<String, String>(parent._captures);
        }

        public Scope OpenTag(string tag)
        {
            return new Scope(this, tag);
        }

        public Scope EndTag()
        {
            if (!String.IsNullOrEmpty(_text))
            {
                var column = _job.GetCaptureColumn(_currentPath);
                if (column != null)
                {
                    _captures[column.Name] = column.Capture(_text);
                }
            }

            if (_inRow && (!_parent._inRow))
            {
                // We are closing a row
                _job.Writer.WriteRow(_captures);
            }

            return _parent;
        }

        public Scope Attribute(string name, string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                var attributePath = _currentPath + "/@" + name;
                var column = _job.GetCaptureColumn(attributePath);
                if (column != null)
                {
                    _captures[column.Name] = column.Capture(value);
                }
            }
            return this;
        }

        public Scope Text(string text)
        {
            _text += text.Trim();
            return this;
        }
    }
}

