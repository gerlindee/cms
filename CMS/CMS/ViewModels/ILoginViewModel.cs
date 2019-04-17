﻿using CMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.ViewModels
{
    interface ILoginViewModel
    {
        bool CheckUser(IPCMemberService pcmemberService);
        void SetCoockie(string username, bool rememberMe);
    }
}
