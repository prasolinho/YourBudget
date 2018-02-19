using System;

namespace YourBudget.Core.Domain
{
    public class MoneyOperation
    {
        public decimal Amount { get; protected set; }
        public string Description { get; protected set; }
        public MoneyOperationCategory Category { get; protected set; }
        public DateTime OperationDate { get; protected set; }

        // Do jakiego portfela operacja
        // TODO: Jeśli tak zostawię to dla operacji bankowych muszę zrobić inny typ operacji. Operacje portfelowe mogę przenosić między różne portfele, a bankowych nie.
        public Wallet Wallet { get; protected set; }

        protected MoneyOperation()
        {
            
        }
    }
}