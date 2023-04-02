﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
       
        public string UserName { get; set;}
        public string Email { get; set;}
        public List<string> Roles { get; set; }
        public bool IsVerfied { get; set; }
        public string JWToken { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }

    }
}
