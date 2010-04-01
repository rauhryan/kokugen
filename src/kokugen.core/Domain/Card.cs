using System;
using Kokugen.Core.Domain;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Domain
{
    [Serializable]
    public class Card : Entity
    {
        [Required]
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual int TimeEstimate { get; set; }
        [Required]
        public virtual Project Project { get; set; }
        public virtual CardStatus Status { get; set; }
    }

    public class CardStatus : Enumeration
    {
        public static CardStatus New = new CardStatus(1, "New");
        public static CardStatus Complete = new CardStatus(2, "Complete");
        private CardStatus(int value, string displayName) : base(value, displayName)
        {
            
        }
    }
}