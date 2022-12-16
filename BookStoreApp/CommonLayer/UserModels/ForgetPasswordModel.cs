using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.UserModels
{
    public class ForgetPasswordModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
