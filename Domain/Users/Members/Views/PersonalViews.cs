using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Members.Views
{
    public class PersonalViews
    {
        private readonly IDictionary<Guid, PersonalView> _views;

        public PersonalViews()
        {
            _views = new Dictionary<Guid, PersonalView>();
        }

        public PersonalViews(params PersonalView[] views)
            : this((IEnumerable<PersonalView>)views)
        {
        }

        public PersonalViews(IEnumerable<PersonalView> views)
            : this()
        {
            foreach (var view in views)
                _views.Add(view.Id, view);
        }

        public PersonalView this[Guid? id]
        {
            get
            {
                if (id == null)
                    return PersonalView.DefaultView;
                PersonalView view;
                _views.TryGetValue(id.Value, out view);
                return view ?? PersonalView.DefaultView;
            }
        }

        public int GetCount(IEnumerable<Guid> ids, PersonalContactDegree degree)
        {
            return (from v in _views
                    where ids.Contains(v.Key)
                          && v.Value.ActualContactDegree == degree
                    select v.Key).Count();
        }
    }
}