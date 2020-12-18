using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SupportAPI.Services.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string OdkServerUrl { get; set; }
        public string OdkUsername { get; set; }
        public string OdkPassword { get; set; }
    }
}