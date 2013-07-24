using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Industries
{
    [Serializable]
	public class Industry
        : IHasId<Guid>
    {
	    private IList<string> _aliases;
        private IList<string> _urlAliases;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string UrlName { get; set; }
        public string KeywordExpression { get; set; }

	    public IList<string> Aliases
	    {
	        get
	        {
                if (_aliases == null)
                    _aliases = new List<string>();
	            return _aliases;
	        }
            set
            {
                _aliases = value;
            }
	    }

        public IList<string> UrlAliases
        {
            get
            {
                if (_urlAliases == null)
                    _urlAliases = new List<string>();
                return _urlAliases;
            }
            set
            {
                _urlAliases = value;
            }
        }

        public IEnumerable<string> AllNames
        {
            get { return new [] { Name }.Concat(Aliases); }
        }

        public IEnumerable<string> AllUrlNames
        {
            get { return new[] { UrlName }.Concat(UrlAliases); }
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Industry);
        }

        public bool Equals(Industry obj)
	    {
	        if (ReferenceEquals(null, obj))
	            return false;
	        if (ReferenceEquals(this, obj))
	            return true;
	        return obj.Id.Equals(Id);
	    }

	    public override int GetHashCode()
	    {
            return Id.GetHashCode();
	    }
    }
}
