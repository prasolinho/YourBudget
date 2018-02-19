using System;

namespace YourBudget.Core.Domain
{
    public class MoneyOperationCategory // ValueObject [???]
    {
        public string Name { get; protected set; }

        public string ParentName { get; protected set; }

        public MoneyOperationCategory(string name, string parentName)
        {
            SetName(name);
            SetParentName(parentName);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
        }

        private void SetParentName(string parentName)
        {
            if (string.IsNullOrWhiteSpace(parentName))
            {
                throw new ArgumentNullException(nameof(parentName));
            }
            ParentName = parentName;
        }
    }
}