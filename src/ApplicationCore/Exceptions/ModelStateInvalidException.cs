using System;

namespace ApplicationCore.Exceptions
{
    public class ModelStateInvalidException : Exception
    {
        public ModelStateInvalidException(string message, string name) : base($"Invalid Model State for {name}: {message}")
        {

        }
    }
}
