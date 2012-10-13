using System;
using System.Collections.Generic;

namespace Aaron.Core.Security.Authentication
{
    public interface IAuthentication
    {
        void SignIn();
        void SignUp();
    }
}
