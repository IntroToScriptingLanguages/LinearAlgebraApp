using System;

namespace LinearAlgebraApp
{
	public class SizeErrorException : Exception
	{
		public SizeErrorException () : base("A size error occured!  The two vectors are of different sizes!")
		{}
	}
}

