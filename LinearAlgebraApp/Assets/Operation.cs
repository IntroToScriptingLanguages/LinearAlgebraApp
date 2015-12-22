using System;

namespace LinearAlgebraApp
{
	public class Operation : Element
	{
		public string value { get; set; }

		public Operation (string op_to_put)
		{
			value = op_to_put;
		}

		public override string printElement ()
		{
			return value;
		}

		public override bool isOperation ()
		{
			return true;
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

