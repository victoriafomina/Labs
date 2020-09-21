using System;

namespace Sets
{
    /// <summary>
    /// The exception that is thrown when the Set is empty.
    /// </summary>
    [Serializable]
    class EmptySetException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the EmptyListException class with the specified error message.
        /// </summary>
        public EmptySetException(string message) : base(message) { }
    }
}
