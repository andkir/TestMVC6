using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace theworld50.Services
{
    public class DebugMailService : IMailService
    {
        public bool SendMail(string to, string from, string subject, string message)
        {
            Debug.WriteLine($"Sending to{to} from {from}. Subject: {subject}");

            return true;
        }
    }
}
