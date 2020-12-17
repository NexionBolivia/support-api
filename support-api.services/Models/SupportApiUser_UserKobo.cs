using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SupportAPI.Services.Models
{
    public class SupportApiUser_UserKobo
    {
        public string Username { get; set; }
        public SupportApiUser SupportApiUser { get; set; }

        public string Name { get; set; }
        public UserKobo UserKobo { get; set; }
    }
}