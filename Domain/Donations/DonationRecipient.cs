﻿using System;

namespace LinkMe.Domain.Donations
{
    public class DonationRecipient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
