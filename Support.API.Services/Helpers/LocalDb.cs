using System.Collections.Generic;

public static class LocalDb
{
    public static Dictionary<string, string> Values = new Dictionary<string, string>()
    {
        { "SUPPORT_DB_SERVER", "localhost" },
        { "SUPPORT_DB_INTERNAL_SERVER", "localhost" },
        { "SUPPORT_DB_PORT", "5432" },
        { "SUPPORT_DB_INTERNAL_PORT", "5432" },
        { "SUPPORT_DB_NAME", "support" },
        { "SUPPORT_DB_USER", "user" },
        { "SUPPORT_DB_PASSWORD", "password" }
    };
}