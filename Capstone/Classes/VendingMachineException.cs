using System;
using System.Runtime.Serialization;

namespace Capstone.Classes
{
    [Serializable]
    public class VendingMachineException : Exception
    {
        public VendingMachineException()
        {
        }

        public VendingMachineException(string message) : base(message)
        {
        }

        public VendingMachineException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VendingMachineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}