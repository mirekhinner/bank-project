using System;

namespace Bank.Objects
{
    public class Bill
    {
        public Int32 BillNumber { get; set; }
        public Guid Guid { get; set; }
        public Int32 Balance { get; set; }
        public Boolean Valid { get; set; }
    }
}