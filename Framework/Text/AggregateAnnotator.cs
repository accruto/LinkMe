using System;

namespace LinkMe.Framework.Text
{
    public class AggregateAnnotator
        : ITextAnnotator, IDisposable
    {
        private readonly ITextAnnotator[] _annotators;

        public AggregateAnnotator(ITextAnnotator[] annotators)
        {
            _annotators = annotators;
        }

        public void AnnotateText(TextFragment textFragment)
        {
            foreach (var annotator in _annotators)
                annotator.AnnotateText(textFragment);
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var annotator in _annotators)
                {
                    var disposable = annotator as IDisposable;
                    if (disposable != null)
                        disposable.Dispose();
                }
            }
        }
    }
}