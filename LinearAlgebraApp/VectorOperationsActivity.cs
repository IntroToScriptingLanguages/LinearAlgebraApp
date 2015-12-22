
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

/*
	HOW EQUATION STRINGS WORK:

	Equations are represented in this program as postfix strings.  "Q" is used as a delimiter to parse operators and operands, and always come before the first operand.
	Vectors are parsed as-is: commas and right brackets separate numbers.

	For example:
	
	4 * {3, 5, -12} is represented as:
	
		 Q 4 Q {3, 5, -12 } Q * (ignoring white space)
		
	{12, 4, 2} + {34, 2, 5} * {2, 4, 6} is represented as:
	
		Q {12, 4, 2 } Q {34, 2, 5 } Q {2, 4, 6 } Q * Q + (ignoring white space)

	Keeping all values at 5 decimal places max is highly suggested.
*/

namespace LinearAlgebraApp
{
	[Activity (Label = "VectorOperationsActivity")]			
	public class VectorOperationsActivity : Activity
	{

		Stack<string> operations;
		Stack<Element> solution;
		//string[] values;
		//string infix_string, postfix_string, value;
		//string value;
		List<Element> infix, postfix;
		string answer;
		Element element1, element2;
		EditText input;
		TextView output;
		Button leftBracket, norm, plus, times, minus, back, comma, equal, clear, menu;

		bool is_Vector; //Whether we are presently in a vector or not!  Also tells if previous input was a vector
		bool is_Norm; //Whether the vector we're in is a norm or not!

		List<Double> new_vector;
		//Vector vector1, vector2;



		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Instantiating variables...

			SetContentView (Resource.Layout.VectorOperations);
			leftBracket = FindViewById<Button> (Resource.Id.leftbracket); //This is VECTOR- it creates a new vector!  Used to use brackets, but decided against it as it was too clunky
			norm = FindViewById<Button> (Resource.Id.norm); //Place as a substitute as VECTOR or before any operator to return the norm of this vector
			plus = FindViewById<Button> (Resource.Id.plus);
			times = FindViewById<Button> (Resource.Id.times);
			minus = FindViewById<Button> (Resource.Id.minus);
			back = FindViewById<Button> (Resource.Id.back);
			comma = FindViewById<Button> (Resource.Id.comma); 
			equal = FindViewById<Button> (Resource.Id.equal);
			clear = FindViewById<Button> (Resource.Id.clear);
			menu = FindViewById<Button> (Resource.Id.menu);
			input = FindViewById<EditText> (Resource.Id.editText1);
			output = FindViewById<TextView> (Resource.Id.textView1);

			operations = new Stack<string> (); //Used for setting up the postfix stack.
			solution = new Stack<Element> (); //Used for postfix calculations.
			is_Vector = false;

			infix = new List<Element> ();
			postfix = new List<Element> ();

			//What all the keys do!
			leftBracket.Click += insertLeftBracket; //VECTOR
			norm.Click += insertNorm; //NORM

			plus.Click += insertAdd; //+
			times.Click += insertMultiply; //*

			comma.Click += insertComma; //,
			equal.Click += calculate; //=

			clear.Click += clearData; //CLEAR
			menu.Click += returnToMenu; //MENU

		}

		private void insertAdd(object sender, EventArgs ea) //Adds plus sign into string, and number in input
		{
			if (is_Vector) {  //Add vector
				if (input.Text == "") { //No input- assume 0 and terminate matrix
					new_vector.Add(0);
				}
				else //There is input- add it to infix string
				{
					new_vector.Add(Convert.ToDouble(input.Text));
				}

				//Convert list to vector or norm here!
				Vector vector_to_add = new Vector(new_vector);
				Element element_to_add;

				if (is_Norm) { //Is a norm
					element_to_add = new Scalar(vector_to_add.getNorm ());
					is_Norm = false;
				}
				else { //Is a vector
					element_to_add = vector_to_add;
				}

				//Adds vector/norm to infix and postfix stacks
				infix.Add(element_to_add);
				postfix.Add (element_to_add);

				is_Vector = false;


				//We push + onto the stack.  If the top value is a *, we pop it until the top value is not *.
				while (operations.Count != 0 && operations.Peek().Equals("*")) //The top of the stack is multiplication
				{
					postfix.Add (new Operation ("*"));
					operations.Pop();
				}

			}
			else if (input.Text != "") { //Add scalar
				infix.Add(new Scalar(Convert.ToDouble(input.Text)));
				postfix.Add (new Scalar(Convert.ToDouble(input.Text)));

				//We push + onto the stack.  If the top value is a *, we pop it until the top value is not *.
				while (operations.Count != 0 && operations.Peek().Equals("*")) //The top of the stack is multiplication
				{
					postfix.Add (new Operation ("*"));
					operations.Pop();
				}
			}

			operations.Push ("+");

			infix.Add(new Operation("+"));

			replaceText (makeOpString(infix));

			input.Text = "";
		}

		private void insertMultiply(object sender, EventArgs ea) //Add multiplication sign into string, and number in input
		{
			if (is_Vector) {  //Multiply vector
				if (input.Text == "") { //No input- assume 0
					new_vector.Add(0);
				}
				else //There is input- add it to infix string
				{
					new_vector.Add(Convert.ToDouble(input.Text));
				}

				//Convert list to vector or norm here!
				Vector vector_to_add = new Vector(new_vector);
				Element element_to_add;

				if (is_Norm) { //Is a norm
					element_to_add = new Scalar(vector_to_add.getNorm ());
					is_Norm = false;
				}
				else { //Is a vector
					element_to_add = vector_to_add;
				}

				//Adds vector/norm to infix and postfix stacks
				infix.Add(element_to_add);
				postfix.Add (element_to_add);

				is_Vector = false;


			}
			else if (input.Text != "") { //Multiply scalar
				infix.Add(new Scalar(Convert.ToDouble(input.Text)));
				postfix.Add (new Scalar(Convert.ToDouble(input.Text)));

			}

			//We push * onto the stack.
			operations.Push ("*");

			infix.Add(new Operation("*"));

			replaceText (makeOpString(infix));

			input.Text = "";
		}

		private void insertLeftBracket(object sender, EventArgs ea) //Starts a new Vector.  Also handles the "Q" delimiters.
		{
			if (!(is_Vector)) {
				new_vector = new List<Double> ();
				is_Vector = true;
				replaceText (makeOpString(infix));
			}
		}

		private void insertComma(object sender, EventArgs ea) //Add comma to string, and number in input, also sets current input as vector
		{
			if (input.Text != "") {
				if (!(is_Vector)) { //If not a vector already, we'll make it so!
					new_vector = new List<Double> ();
					is_Vector = true;
				} 

				new_vector.Add (Convert.ToDouble (input.Text));
				replaceText (makeOpString (infix));
				input.Text = "";
			}
		}

		private void insertNorm(object sender, EventArgs ea) //Make the current vector a norm calculation (or creates one if it's not already)
		{
			if (!(is_Vector)) { //If we're not a vector, creates a vector with norm turned on.
				new_vector = new List<Double> ();
				is_Vector = true;
			} 

			is_Norm = true;
			replaceText (makeOpString(infix));
		}

		private void calculate(object sender, EventArgs ea) //Performs the actual calculations!
		{
			if (is_Vector || input.Text != "") { 
				
				if (is_Vector) { //Input a vector
					if (input.Text == "") { //No input- assume 0
						new_vector.Add(0);
					}
					else //There is input- add it to infix string
					{
						new_vector.Add(Convert.ToDouble(input.Text));
					}

					//Convert list to vector or norm here!
					Vector vector_to_add = new Vector(new_vector);
					Element element_to_add;

					if (is_Norm) { //Is a norm
						element_to_add = new Scalar(vector_to_add.getNorm ());
						is_Norm = false;
					}
					else { //Is a vector
						element_to_add = vector_to_add;
					}

					//Adds vector/norm to infix and postfix stacks
					infix.Add(element_to_add);

					postfix.Add(element_to_add);

					is_Vector = false;
				} 
				else { //Input a scalar
					infix.Add(new Scalar(Convert.ToDouble(input.Text)));
					postfix.Add(new Scalar(Convert.ToDouble(input.Text)));
				}

				while (operations.Count != 0) { //Keep popping the stack until it's empty!
					postfix.Add(new Operation(operations.Pop ()));
				}

				infix.Add(new Operation("="));

				replaceText (makeOpString(infix));

				//Here's where actual calculations begin!
				foreach (Element value in postfix) { //The first value is just empty space, so we start on the second!

					if (value.isOperation()) { //It's an operator!  Pop the stack twice and operate on them!
						element2 = solution.Pop();
						element1 = solution.Pop();

						//Scalar on Scalar
						if (!(element1.isVector ()) && !(element2.isVector ())) {
							try{
								solution.Push (performOperation ((Scalar)element1, (Scalar)element2, (Operation)value));
							}
							catch (UnrecognizedSymbolException e) {
								replaceText("There was an error!  This operator cannot be used on two scalars!");
								clearDataWithOutput ();
								return;
							}
						} 
						else if (element1.isVector () && element2.isVector ()) { //Vector on Vector
							try
							{
								Element result = performVectorOperation ((Vector)element1, (Vector)element2, (Operation)value);
								solution.Push (result);
							}
							catch (SizeErrorException e)
							{
								replaceText("There was an error!  You cannot operate on two vectors of different sizes!");
								clearDataWithOutput ();
								return;
							}
							catch (UnrecognizedSymbolException e) {
								replaceText("There was an error!  This operator cannot be used on two vectors!");
								clearDataWithOutput ();
								return;
							}
						} 
						else { //Scalar on Vector
							if (((Operation) value).value != "*") { //You cannot perform any operation other than multiplication from a scalar onto a vector
								replaceText("There was an error!  You cannot perform this operation from a scalar to a vector!");
								clearDataWithOutput ();
								return;
							}

							try{
								solution.Push(performScalarMultiplication(element1, element2));
							}
							catch (UnrecognizedSymbolException e) {
								replaceText("There was an error!  This operator cannot be used on a scalar and a vector!");
								clearDataWithOutput ();
								return;
							}

						}
					} 
					else { //It's a scalar or vector!  Push it onto the solution stack!
						solution.Push (value);
					}
				}

				if (solution.Count != 1) { //Oops, there was a messup!  There should be exactly one value left on the stack!
					replaceText("There was an error!  There were "+solution.Count+" solutions!");
					clearDataWithOutput ();
					return;
				}



				answer = solution.Pop().printElement(); //This is the answer!

				updateText(""+answer); //Adds the answer to the TextView
				clearDataWithOutput();
			}
		}

		private Vector performScalarMultiplication(Element vec1, Element vec2) //Handles scalar times vector, return vector
		{
			Vector vect;
			Scalar scalar;
			if (vec1.isVector()) { //Vector 1 vector, vector 2 scalar
				scalar = new Scalar(Convert.ToDouble (vec2));
				vect = (Vector) vec1;
			}
			else { //Vector 2 vector, vector 1 scalar
				scalar = new Scalar(Convert.ToDouble (vec1));
				vect = (Vector) vec2;
			}

			vect *= scalar;
				
			return vect;

		}

		//VECTOR OPERATIONS
		private Element performVectorOperation(Vector vector1, Vector vector2, Operation op) //Handles vector multiplication, addition, and subtraction
		{
			Vector answer_vector;
			if (vector1.height != vector2.height) { //Two vectors are not the same size!  We can't operate on them!
				throw new SizeErrorException();
			}
			switch (op.value) {
			case "+": //Addition
				return (vector1 + vector2);
			case "*": //Vector dot product
				double dot_product = vector1 * vector2;
				return new Scalar (dot_product);
			default:
				throw new UnrecognizedSymbolException (op);
			}
		}

		//SCALAR OPERATIONS
		private Scalar performOperation(Scalar num1, Scalar num2, Operation op) //Performs the specified scalar operation.
		{

			double number1 = num1.value;
			double number2 = num2.value;
			switch (op.value) {
			case "+": //Addition
				return new Scalar(number1 + number2);
			case "-": //Subtraction
				return new Scalar(number1 - number2);
			case "*":
				return new Scalar(number1 * number2);
			case "/":
				return new Scalar(number1 / number2);
			case "%":
				return new Scalar(number1 % number2);
			default:
				throw new UnrecognizedSymbolException (op);
			}
		}

		//SUPPORT METHODS
		private string makeOpString(List<Element> convertee) //Converts a stack of elements into an operation string.  Automatically appends the new vector at the end!
		{
			string result = "";

			foreach (Element e in convertee) {
				result += e.printElement() + " ";
			}

			if (is_Vector) { //Append the data in the new_vector!
				//Creating format string
				string matrix_string = "{";
				foreach (double d in new_vector) {
					matrix_string += d + ", ";
				}

				if (is_Norm) { //Append an additional "|" before the list to show it's a string!
					result += "|";
				}

				result += matrix_string;
			}

			return result;
		}

		private void clearData(object sender, EventArgs ea) //Clears everything!
		{
			infix.Clear ();
			postfix.Clear ();
			input.Text = "";
			operations.Clear ();
			solution.Clear ();
			answer = "";
			is_Vector = false;

			replaceText ("");
		}

/*		private bool isNorm (string s) //Checks to see if a string is a norm
		{
			return s [s.Length - 1] == '|';
		}

		private bool isVector(string s) //Checks to see if string is a vector
		{
			return s [0] == '{';
		}*/

		private void clearDataWithOutput() //Clears everything, but leaves output.Text intact.  Also, not a delegate!
		{
			infix.Clear ();
			postfix.Clear ();
			input.Text = "";
			operations.Clear ();
			solution.Clear ();
			answer = "";
		}

		private void updateText(string text) //Updates the text displayed in output
		{
			output.Text += text;
		}

		private void replaceText(string text) //Completley replaces text in output
		{
			output.Text = text;
		}

		private void returnToMenu(object sender, EventArgs ea) //Returns to the main menu!
		{
			StartActivity (typeof(MainActivity));
		}
	}
}

