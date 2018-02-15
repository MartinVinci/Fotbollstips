using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fotbollstips.Objects
{
    public class ParticipateResult
    {
        public bool SavedInDatabase { get; set; }
        public bool EmailSent { get; set; }
        public string EmailAddress { get; set; }

        public ParticipateResult(bool savedIndatabase, bool emailSent, string emailAddress)
        {
            SavedInDatabase = savedIndatabase;
            EmailSent = emailSent;
            EmailAddress = emailAddress;
        }
    }
}