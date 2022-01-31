using System;
using System.Collections.Generic;
using System.Text;

namespace Feedback.API.Authentication
{
    public interface IRolesManager
    {
        string[] GetUserRoles(string userId);
    }

    public class RolesManager : IRolesManager
    {
        static string adminUser = "BAF003C2-8CB4-4AE8-8A55-198660830E80";
        public string[] GetUserRoles(string userId)
        {
            return userId == adminUser ? new string[] { "Admin" } : new string[0];
        }
    }
}
