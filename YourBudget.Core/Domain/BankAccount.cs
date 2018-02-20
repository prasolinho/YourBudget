using System;

namespace YourBudget.Core.Domain
{
    public class BankAccount
    {
        /// <summary>
        /// Nazwa konta w banku np. eKonto (mBank), "Dobre konto" (Millenium)
        /// </summary>
        /// <returns></returns>
        public string OfficialName { get; protected set; }

        /// <summary>
        /// Własna nazwa
        /// </summary>
        /// <returns></returns>
        public string CustomName { get; protected set; }

        /// <summary>
        /// Saldo
        /// </summary>
        /// <returns></returns>
        public decimal Balance { get; protected set; }

        /// <summary>
        /// Powiązany portfel - do niego będą dodawane np. wypłaty z bankomatu.
        /// </summary>
        /// <returns></returns>
        public Wallet RelatedWalled { get; protected set; }

        protected BankAccount()
        {

        }

        public BankAccount(string officialName, string customName, decimal balance, Wallet relatedWallet)
        {
            
        }

        private void SetOfficialName(string officialName)
        {
            if (string.IsNullOrWhiteSpace(officialName))
            {
                throw new ArgumentNullException(nameof(officialName));
            }
            OfficialName = officialName;            
        }

        private void SetCustomName(string customName)
        {
            if (string.IsNullOrWhiteSpace(customName))
            {
                throw new ArgumentNullException(nameof(customName));
            }
            CustomName = customName;            
        }

        public void SetBalance(decimal balance)
        {
            // Nie wiem jaka walidacja? Na minusie może być, więc nie sprawdzam.
            if (Balance == balance)
            {
                return;
            }
            Balance = balance;
        }

        public void SetRelatedWallet(Wallet wallet) // Najlepiej z punktu widzenia projektowania bazy danych to było by przekazać samo ID
        {
            if (RelatedWalled == wallet)
            {
                return;
            }
            RelatedWalled = wallet;
        }
    }
}