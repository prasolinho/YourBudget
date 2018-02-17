using System;

namespace YourBudget.Core.Domain
{
    public class Wallet
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public decimal Amount { get; protected set; }

        // TODO: tyle właściwości na początek wystarczy, później można by dodać walutą, czy to zaliczać jako oszczędności czy środki bieżące

        protected Wallet()
        {

        }

        /// <summary>
        /// Create empty wallet.
        /// </summary>
        /// <param name="name"></param>
        public Wallet(string name)
        {
            Name = name;
            Amount = 0;
        }

        /// <summary>
        /// Create wallet with money inside.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        public Wallet(string name, decimal amount)
        {
            Name = name;
            Amount = amount;
        }
    }
}