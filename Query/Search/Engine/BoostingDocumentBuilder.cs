using System.Collections.Generic;
using System.Text;
using org.apache.lucene.analysis;
using org.apache.lucene.document;

namespace LinkMe.Query.Search.Engine
{
    internal class BoostingDocumentBuilder
    {
        private readonly Dictionary<string, FieldBuilder> _fieldMap = new Dictionary<string, FieldBuilder>();
        private readonly Analyzer _analyzer;

        public BoostingDocumentBuilder(Analyzer analyzer)
        {
            _analyzer = analyzer;
        }

        public void AddText(string fieldName, string text)
        {
            AddText(fieldName, text, float.NaN);
        }

        public void AddText(string fieldName, string text, float boost)
        {
            var field = GetField(fieldName);
            field.AddText(text, boost);
        }

        public void CopyTo(Document document)
        {
            foreach (var fieldEntry in _fieldMap)
            {
                var fieldName = fieldEntry.Key;
                var field = fieldEntry.Value;

                if (field.HasBoosts())
                {
                    var reader = new java.io.StringReader(field.GetText());
                    var tokenStream = _analyzer.tokenStream(fieldName, reader);
                    document.add(new Field(fieldName, new BoostingTokenFilter(tokenStream, field.GetStartOffsets(), field.GetBoosts())));
                }
                else
                {
                    document.add(new Field(fieldName, field.GetText(), Field.Store.NO, Field.Index.ANALYZED));
                }
            }
        }

        private FieldBuilder GetField(string fieldName)
        {
            FieldBuilder field;
            if (!_fieldMap.TryGetValue(fieldName, out field))
            {
                field = new FieldBuilder();
                _fieldMap.Add(fieldName, field);
            }

            return field;
        }

        private class FieldBuilder
        {
            private readonly StringBuilder _text = new StringBuilder();
            private readonly List<int> _startOffsets = new List<int>();
            private readonly List<float> _boosts = new List<float>();

            public void AddText(string text, float boost)
            {
                if (string.IsNullOrEmpty(text))
                    return;

                var lastBoost = (_boosts.Count > 0) ? _boosts[_boosts.Count - 1] : float.NaN;
                if (!boost.Equals(lastBoost))
                {
                    _startOffsets.Add(_text.Length);
                    _boosts.Add(boost);
                }

                _text.Append(text);
                _text.Append(" ");
            }

            public bool HasBoosts()
            {
                return _startOffsets.Count > 0;
            }

            public string GetText()
            {
                return _text.ToString();
            }

            public int[] GetStartOffsets()
            {
                return _startOffsets.ToArray();
            }

            public float[] GetBoosts()
            {
                return _boosts.ToArray();
            }
        }
    }
}
