using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkMe.Domain.Users.Members.Views
{
    public abstract class ProfessionalViewCollection<TView>
        : IEnumerable<TView>
        where TView : ProfessionalView, new()
    {
        private int? _contactCredits = 0;
        private readonly IDictionary<Guid, TView> _views;

        private static readonly TView DefaultView = new TView();

        protected ProfessionalViewCollection()
        {
            _views = new Dictionary<Guid, TView>();
        }

        public int? ContactCredits
        {
            get { return _contactCredits; }
            internal set { _contactCredits = value; }
        }

        public void Add(TView view)
        {
            _views.Add(view.Id, view);
        }

        public TView this[Guid? id]
        {
            get
            {
                if (id == null)
                    return DefaultView;
                TView view;
                _views.TryGetValue(id.Value, out view);
                return view ?? DefaultView;
            }
        }

        IEnumerator<TView> IEnumerable<TView>.GetEnumerator()
        {
            return _views.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _views.Values.GetEnumerator();
        }
    }

    public class ProfessionalViews
        : ProfessionalViewCollection<ProfessionalView>
    {
    }
}