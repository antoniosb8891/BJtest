using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace BJtest.Common.Exceptions
{
    public class InternalException : Exception
    {
        public InternalException(string message) : base(message)
        {
        }
    }
}
