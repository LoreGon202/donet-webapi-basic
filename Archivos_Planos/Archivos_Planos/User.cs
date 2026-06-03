using System;
using System.Collections.Generic;
using System.Text;

namespace Flat_Files
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }
    }
}