using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkMe.Framework.Content.Templates
{
    internal class ViewEngine
    {
        private readonly ITextTemplateEngine _textTemplateEngine;

        public ViewEngine(ITextTemplateEngine textTemplateEngine)
        {
            _textTemplateEngine = textTemplateEngine;
        }

        public string GetCopyText(TemplateContext context, TemplateProperties properties)
        {
            return _textTemplateEngine.GetCopyText(context, properties);
        }
    }

    internal class ViewEngines
        : IEnumerable<KeyValuePair<string, ViewEngine>>
    {
        private readonly IDictionary<string, ViewEngine> _engines = new Dictionary<string, ViewEngine>();

        public void Add(string mimeType, ViewEngine engine)
        {
            _engines[mimeType ?? string.Empty] = engine;
        }

        public ViewEngine this[string mimeType]
        {
            get
            {
                ViewEngine engine;
                _engines.TryGetValue(mimeType, out engine);
                return engine;
            }
        }

        IEnumerator<KeyValuePair<string, ViewEngine>> IEnumerable<KeyValuePair<string, ViewEngine>>.GetEnumerator()
        {
            return _engines.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _engines.GetEnumerator();
        }
    }

    public class CopyItemEngine
    {
        private readonly ViewEngines _viewEngines = new ViewEngines();
        private readonly ITextTemplateEngine _textTemplateEngine;

        internal CopyItemEngine(ITextTemplateEngine textTemplateEngine)
        {
            _textTemplateEngine = textTemplateEngine;
        }

        internal ViewEngines ViewEngines
        {
            get { return _viewEngines; }
        }

        internal string GetCopyText(TemplateContext context, TemplateProperties properties)
        {
            return _textTemplateEngine.GetCopyText(context, properties);
        }
    }

    internal class CopyItemEngines
    {
        private readonly IDictionary<Guid, CopyItemEngine> _engines = new Dictionary<Guid, CopyItemEngine>();

        public void Add(Guid? verticalId, CopyItemEngine engine)
        {
            _engines[verticalId ?? Guid.Empty] = engine;
        }

        public CopyItemEngine this[Guid? verticalId]
        {
            get
            {
                CopyItemEngine engine;
                _engines.TryGetValue(verticalId ?? Guid.Empty, out engine);
                return engine;
            }
        }
    }

    internal class Engine
    {
        private readonly CopyItemEngines _engines = new CopyItemEngines();

        public CopyItemEngines CopyItemEngines
        {
            get { return _engines; }
        }
    }

    internal class Engines
    {
        private readonly IDictionary<string, Engine> _engines = new Dictionary<string, Engine>();

        public void Add(string name, Engine engine)
        {
            _engines[name] = engine;
        }

        public Engine this[string name]
        {
            get
            {
                Engine engine;
                _engines.TryGetValue(name ?? string.Empty, out engine);
                return engine;
            }
        }
    }
}