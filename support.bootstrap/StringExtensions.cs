using System;

namespace support.bootstrap
{
    public static class StringExtensions
    {
        public static string? ReplaceConnectionStringEnvVars(this string? toReplace)
        {
            return toReplace == null ? 
                      toReplace : toReplace
                        .ReplaceWithValue("SUPPORT_DB_SERVER")
                        .ReplaceWithValue("SUPPORT_DB_INTERNAL_SERVER")                        
                        .ReplaceWithValue("SUPPORT_DB_PORT")
                        .ReplaceWithValue("SUPPORT_DB_INTERNAL_PORT")
                        .ReplaceWithValue("SUPPORT_DB_NAME")
                        .ReplaceWithValue("SUPPORT_DB_USER")
                        .ReplaceWithValue("SUPPORT_DB_PASSWORD");
        }

        private static string ReplaceWithValue(this string toReplace, string key)
        {
            var envVarValue = Environment.GetEnvironmentVariable(key);
            return (envVarValue != null) ? toReplace.Replace(key, envVarValue) : toReplace;
        }
    }
}
