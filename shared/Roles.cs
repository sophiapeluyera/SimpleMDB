using System;

namespace SimpleMDB
{
    public static class Roles
    {
        public static readonly string ADMIN = "admin";
        public static readonly string USER = "user";

        public static readonly string[] ROLES = new[] { ADMIN, USER };

        public static bool Check(string? role)
        {
            return Array.Exists(ROLES, r => r == role);
        }
    }
}
