using System;

namespace LinearAlgebraApp
{
	public class Scalar : Element
	{
		public double value { get; set; }

		public Scalar (double s)
		{
			value = s;
		}

		public override string printElement ()
		{
			return "" + value;
		}

		public override bool isOperation ()
		{
			return false;
		}

		public override bool isVector ()
		{
			return false;
		}

		public override Object getValue()
		{
			return value;
		}
	}
}

