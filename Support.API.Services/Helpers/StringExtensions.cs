using System;

namespace Support.API.Services.Helpers
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

        public static string? ReplaceConnectionStringEnvVarsForKoboForm(this string? toReplace)
        {
            return toReplace == null ?
                      toReplace : toReplace
                        .ReplaceWithValue("KOBO_DB_SERVER")
                        .ReplaceWithValue("KOBO_DB_PORT")
                        .ReplaceWithValue("KOBO_DB_NAME")
                        .ReplaceWithValue("KOBO_DB_USER")
                        .ReplaceWithValue("KOBO_DB_PASSWORD");
        }

        public static string? ReplaceConnectionStringEnvVarsForKoboCat(this string? toReplace)
        {
            return toReplace == null ?
                      toReplace : toReplace
                        .ReplaceWithValue("KOBO_DB_SERVER")
                        .ReplaceWithValue("KOBO_DB_PORT")
                        .ReplaceWithValue("KOBO_CAT_DB_NAME")
                        .ReplaceWithValue("KOBO_DB_USER")
                        .ReplaceWithValue("KOBO_DB_PASSWORD");
        }

        public static string? ReplaceKoboToolboxUri(this string? toReplace)
        {
            return toReplace == null ?
                      toReplace : toReplace
                        .ReplaceWithValue("KOBO_API_SERVER");
        }

        private static string ReplaceWithValue(this string toReplace, string key)
        {
            var envVarValue = Environment.GetEnvironmentVariable(key);
            return (!string.IsNullOrEmpty(envVarValue)) ? toReplace.Replace(key, envVarValue) :
                LocalDb.Values.ContainsKey(key) ? toReplace.Replace(key, LocalDb.Values[key]) : toReplace;
        }
    }
}
