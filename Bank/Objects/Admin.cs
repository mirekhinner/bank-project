using Bank.Types;
using System;

namespace Bank.Objects
{
    public class Admin : User
    {
        public String Password { get; set; }
        public AdminType AdminType { get; set; }
        public String Login { get; set; }
    }
}
