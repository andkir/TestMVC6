﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace theworld50.Services
{
    public interface IMailService
    {
        bool SendMail(string to, string from, string subject, string message);
    }
}
