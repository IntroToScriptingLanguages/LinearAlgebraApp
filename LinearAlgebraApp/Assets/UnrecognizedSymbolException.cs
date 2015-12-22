using System;

namespace LinearAlgebraApp
{
	public class UnrecognizedSymbolException : Exception
	{
		public UnrecognizedSymbolException (Operation op) : base("Error in performing vector operation! Does not recognize symbol: "+op.getValue())
		{
		}
	}
}

