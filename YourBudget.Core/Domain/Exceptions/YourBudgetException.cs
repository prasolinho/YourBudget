using System;

namespace YourBudget.Core.Domain.Exceptions
{
    // Class name after application name
    public abstract class YourBudgetException : Exception
    {
        public string Code { get; }

        protected YourBudgetException()
        {

        }

        public YourBudgetException(string code)
        {
            Code = code;
        }

        public YourBudgetException(string message, params object[] args) : this(string.Empty, message, args)
        {

        }

        public YourBudgetException(string code, string message, params object[] args) : this(null, code, message, args)
        {

        }

        public YourBudgetException(Exception innerException, string message, params object[] args) : this(innerException, string.Empty, message, args)
        {

        }

        public YourBudgetException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
