using System;

namespace Model.Services.Exceptions
{
    /// <summary>
    /// Exception throwed when a product hasn't got enough stock
    /// </summary>
    public class InsufficientStockExcepcion : Exception
    {
        public InsufficientStockExcepcion()
        {
        }

        public InsufficientStockExcepcion(string message)
            : base(message)
        {
        }

        public InsufficientStockExcepcion(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
