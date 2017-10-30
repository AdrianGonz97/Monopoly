using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Cards
{
    abstract class Card
    {
        public String CardName { get; }
        public String Description { get; }

        public abstract void CardAction();
        
    }
}
