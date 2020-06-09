using System;

namespace Bank.Objects
{
    public class Transaction
    {
        public Int32 Id { get; set; }
        public Int32 VariableSymbol { get; set; }
        public DateTime DateTransaction { get; set; }
        public Int32 Payer { get; set; }
        public Int32 Recipient { get; set; }
        public Int32 Amount { get; set; }
        public Boolean Valid { get; set; }
    }
}
