using FluentAssertions;
using NUnit.Framework;
using System;
using Support.API.Services.Helpers;

namespace Support.API.Services.Test
{
    public class StringExtensionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EmptyString()
        {
            "".ReplaceConnectionStringEnvVars().Should().BeEmpty();
        }

        [Test]
        public void NullString()
        {
            string nullString = null;
            nullString.ReplaceConnectionStringEnvVars().Should().BeNull();
        }

        [Test]
        public void ValidString()
        {
            string validString = "Totally Valid";
            validString.ReplaceConnectionStringEnvVars().Should().Be(validString);
        }

        [Test]
        public void NullString_WithEnvVar()
        {
            Environment.SetEnvironmentVariable("SUPPORT_DB_SERVER", "Test var");

            string nullString = null;
            nullString.ReplaceConnectionStringEnvVars().Should().BeNull();
        }

        [Test]
        public void ConnString_WithEnvVars()
        {
            Environment.SetEnvironmentVariable("SUPPORT_DB_SERVER", "server");
            Environment.SetEnvironmentVariable("SUPPORT_DB_PORT", "9999");
            Environment.SetEnvironmentVariable("SUPPORT_DB_NAME", "my_db");
            Environment.SetEnvironmentVariable("SUPPORT_DB_USER", "my_user");
            Environment.SetEnvironmentVariable("SUPPORT_DB_PASSWORD", "my_password");

            string connString = "Server=SUPPORT_DB_SERVER; Port=SUPPORT_DB_PORT; Database=SUPPORT_DB_NAME; Username=SUPPORT_DB_USER; Password=SUPPORT_DB_PASSWORD";
            connString.ReplaceConnectionStringEnvVars().Should().NotBe(connString);
            connString.ReplaceConnectionStringEnvVars().Should().Be("Server=server; Port=9999; Database=my_db; Username=my_user; Password=my_password");
        }

        [Test]
        public void ConnString_WithUnreplacedEnvVars()
        {
            Environment.SetEnvironmentVariable("SUPPORT_DB_SERVER", string.Empty); // This variable should be replaced with default
            LocalDb.Values["SUPPORT_DB_SERVER"] = "myDefaultLocalhost";

            Environment.SetEnvironmentVariable("SUPPORT_DB_PORT", string.Empty);
            LocalDb.Values["SUPPORT_DB_PORT"] = "0000";

            Environment.SetEnvironmentVariable("SUPPORT_DB_NAME", string.Empty);
            LocalDb.Values["SUPPORT_DB_NAME"] = "myDefaultDatabase";

            Environment.SetEnvironmentVariable("SUPPORT_DB_USER", string.Empty);
            LocalDb.Values["SUPPORT_DB_USER"] = "myDefaultUser";

            Environment.SetEnvironmentVariable("SUPPORT_DB_PASSWORD", string.Empty);
            LocalDb.Values["SUPPORT_DB_PASSWORD"] = "myDefaultPassword";

            string connString = "Server=SUPPORT_DB_SERVER; Port=SUPPORT_DB_PORT; Database=SUPPORT_DB_NAME; Username=SUPPORT_DB_USER; Password=SUPPORT_DB_PASSWORD";
            var replaced = connString.ReplaceConnectionStringEnvVars();
            replaced.Should().NotBe(connString);
            replaced.ReplaceConnectionStringEnvVars().Should().Be("Server=myDefaultLocalhost; Port=0000; Database=myDefaultDatabase; Username=myDefaultUser; Password=myDefaultPassword");
        }
    }
}