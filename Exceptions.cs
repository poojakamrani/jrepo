using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpAssignment
{
    class InvalidAmountException : ApplicationException
    {
        public InvalidAmountException(string message) : base(message) { }
    }
    class InsufficientFundException : ApplicationException
    {
        public InsufficientFundException(string message) : base(message) { }
    }
}
