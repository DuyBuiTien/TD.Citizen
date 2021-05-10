﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TD.CongDan.Application.DTOs.Identity
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ProfilePicture { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsActive { get; set; } = false;
    }
}