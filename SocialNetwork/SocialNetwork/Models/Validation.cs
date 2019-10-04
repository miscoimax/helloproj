using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models
{
    [MetadataType(typeof(UserValidation))]
    public partial class User
    {
        //dummy
    }

    public class UserValidation
    {
        //public int user_id { get; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password_hash { get; set; }
    }
}