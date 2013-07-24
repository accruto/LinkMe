namespace LinkMe.Domain.Requests
{
    public enum RequestStatus
    {
        /// <summary>
        /// Not yet sent
        /// </summary>
        NotSent = 0,
        /// <summary>
        /// Sent, but not yet actioned by the recipient and not withdrawn by the sender
        /// </summary>
        Pending = 1,
        /// <summary>
        /// Accepted by the recipient
        /// </summary>
        Accepted = 2,
        /// <summary>
        /// Declined by the recipient
        /// </summary>
        Declined = 3,
        /// <summary>
        /// Revoked by the sender after it has been sent
        /// </summary>
        Revoked = 4,
    }
}