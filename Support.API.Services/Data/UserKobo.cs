using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Support.API.Services.Models
{
    public class UserKobo
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public List<SupportApiUser_UserKobo> SupportApiUser_UserKobo { get; set; }
    }
}