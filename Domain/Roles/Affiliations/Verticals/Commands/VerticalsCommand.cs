using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Affiliations.Verticals.Commands
{
    public class VerticalsCommand
        : IVerticalsCommand
    {
        private readonly IVerticalsRepository _repository;

        public VerticalsCommand(IVerticalsRepository repository)
        {
            _repository = repository;
        }

        void IVerticalsCommand.CreateVertical(Vertical vertical)
        {
            vertical.Prepare();
            vertical.Validate();
            _repository.CreateVertical(vertical);
        }

        void IVerticalsCommand.UpdateVertical(Vertical vertical)
        {
            vertical.Validate();
            _repository.UpdateVertical(vertical);
        }

        Vertical IVerticalsCommand.GetVertical(Guid id)
        {
            return _repository.GetVertical(id, true);
        }

        Vertical IVerticalsCommand.GetVerticalByHost(string host)
        {
            return _repository.GetVerticalByHost(host, true);
        }

        Vertical IVerticalsCommand.GetVerticalByUrl(string url)
        {
            return _repository.GetVerticalByUrl(url, true);
        }

        IList<Vertical> IVerticalsCommand.GetVerticals()
        {
            return _repository.GetVerticals(true);
        }
    }
}