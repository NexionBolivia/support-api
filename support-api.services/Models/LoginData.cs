﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace support_api.services.Models
{
    public class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}