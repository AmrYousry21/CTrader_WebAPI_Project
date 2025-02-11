﻿using Google.Apis.Admin.Directory.directory_v1.Data;
using System.ComponentModel.DataAnnotations;

namespace CTraderMVC.Models
{
    public class User : IUser
    {
        public int PersonId { get; set; }
        [Display(Name= "User Name")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
