using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Asp.Content;

namespace LinkMe.Apps.Asp.Elements
{
    public class HeadInfo
    {
        private static readonly StyleSheetReferences StandardStyleSheetReferences = new StyleSheetReferences();
        private bool _useStandardStyleSheetReferences = true;

        private static readonly JavaScriptReferences StandardJavaScriptReferences = new JavaScriptReferences();

        private StyleSheetReferences _styleSheetReferences;
        private JavaScriptReferences _javaScriptReferences;
        private RssFeedReferences _rssFeedReferences;
        private readonly MetaTags _metaTags = new MetaTags();

        public static void AddStandard(StyleSheetReference reference)
        {
            StandardStyleSheetReferences.Add(reference);
        }

        public static void AddStandard(JavaScriptReference reference)
        {
            StandardJavaScriptReferences.Add(reference);
        }

        public bool UseStandardStyleSheetReferences
        {
            get { return _useStandardStyleSheetReferences; }
            set { _useStandardStyleSheetReferences = value; }
        }

        public MetaTags MetaTags
        {
            get { return _metaTags; }
        }

        public FaviconReference FaviconReference { get; set; }

        public RssFeedReferences RssFeedReferences
        {
            get { return _rssFeedReferences; }
        }

        public IEnumerable<StyleSheetReference> StyleSheetReferences
        {
            get
            {
                if (_useStandardStyleSheetReferences)
                    return _styleSheetReferences == null ? StandardStyleSheetReferences : StandardStyleSheetReferences.Concat(_styleSheetReferences);
                return _styleSheetReferences;
            }
        }

        public IEnumerable<JavaScriptReference> JavaScriptReferences
        {
            get { return _javaScriptReferences == null ? StandardJavaScriptReferences : StandardJavaScriptReferences.Concat(_javaScriptReferences); }
        }

        public void Add(RssFeedReference reference)
        {
            if (_rssFeedReferences == null)
            {
                _rssFeedReferences = new RssFeedReferences {reference};
            }
            else
            {
                if (!IsReferenceIncluded(_rssFeedReferences, reference))
                    _rssFeedReferences.Add(reference);
            }
        }

        public void Add(StyleSheetReference reference)
        {
            if (_useStandardStyleSheetReferences && IsReferenceIncluded(StandardStyleSheetReferences, reference))
                return;

            if (_styleSheetReferences == null)
            {
                _styleSheetReferences = new StyleSheetReferences {reference};
            }
            else
            {
                if (!IsReferenceIncluded(_styleSheetReferences, reference))
                    _styleSheetReferences.Add(reference);
            }
        }

        public void Add(JavaScriptReference reference)
        {
            if (IsReferenceIncluded(StandardJavaScriptReferences, reference))
                return;

            if (_javaScriptReferences == null)
            {
                _javaScriptReferences = new JavaScriptReferences { reference };
            }
            else
            {
                if (!IsReferenceIncluded(_javaScriptReferences, reference))
                    _javaScriptReferences.Add(reference);
            }
        }

        public void InsertBeforeAll(JavaScriptReference reference)
        {
            // If reference exists anywhere, remove it
            //
            if (IsReferenceIncluded(StandardJavaScriptReferences, reference))
                StandardJavaScriptReferences.Remove(reference);

            if (_javaScriptReferences != null)
                if (IsReferenceIncluded(_javaScriptReferences, reference))
                    _javaScriptReferences.Remove(reference);

            // (Re-)add reference before all standard javascript references
            //
            StandardJavaScriptReferences.Insert(reference);
        }

        private static bool IsReferenceIncluded<R>(IEnumerable<R> references, R newReference)
            where R : Reference
        {
            foreach (var reference in references)
            {
                if (reference.Equals(newReference))
                    return true;
            }

            return false;
        }
    }
}
