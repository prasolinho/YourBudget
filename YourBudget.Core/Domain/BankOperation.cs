using System;

namespace YourBudget.Core.Domain
{
    // TODO: właściwości, które występują również w `MoneyOperation` może wynieść do nowej klasy bazowej
    public class BankOperation
    {
        public decimal Amount { get; protected set; }
        public string Description { get; protected set; }
        public MoneyOperationCategory Category { get; protected set; }
        public DateTime OperationDate { get; protected set; }

        public BankAccount BankAccount { get; protected set; }

        protected BankOperation()
        {

        }
    }
}