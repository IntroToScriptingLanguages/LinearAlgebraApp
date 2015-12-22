using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinearAlgebraApp
{
	public class Vector : Matrix //One-dimensional matrix with only height- width is 1!  Always a column vector.
	{

		public Vector (int l) //Creates new empty vector with length l
			: base(l, 1) {}

		public Vector (double[] v) //Creates new vector from array
			: base(v.Length, 1)
		{
			setMatrix(v);
		}

		public Vector (List<Double> v) //Creates new vector from list
			: base(v.Count, 1) //Wanna make sure List.Count returns the list's length
		{
			setMatrix (v.ToArray ());
		}

		public Vector (string vector) //Parses a string into a vector
			: base(0, 1)
		{
			List<double> list = new List<double> (1);
			bool isDecimal = false; //Tells if you're currently parsing a decimal
			bool isNegative = false; //Tells if you're currently parsing a negative
			double temp = 0; //Stores the current int here.
			double dec = 0; //This is the current decimal power here

			foreach (char c in vector)
			{

				if (c == ',' || c == '}') { //It's a comma/end of vector, so we store the int into the list!
					if (isNegative) {
						temp *= -1; //If is negative, we divide by -1
						isNegative = false;
					}

					list.Add (temp);
					isDecimal = false;
					temp = 0;
					dec = 0;
				} 
				else if (Matrix.isInteger(c)) { //It's an integer!  We add it as a digit to temp.
					double convertedChar = Char.GetNumericValue(c); //c converted into an double
					if (isDecimal) { //Take into account the fact this is a decimal
						dec--;
						temp += convertedChar * Math.Pow (10, dec);

					}
					//If this is not a decimal
					else {
						temp *= 10;
						temp += convertedChar;
					}
				} 
				else if (c == '.') { //It's a decimal!  Let's turn on decimal parse mode!
					isDecimal = true;
				} 
				else if (c == '-') { //It's a negative sign! Convert the int to negative
					isNegative = true;
				}
			}
			int count = 0;
			double[,] temp_vector = new double[list.Count, 0];
			foreach (double val in list) {
				temp_vector [count, 0] = val;
				count++;
			}
			matrix = temp_vector;
			height = list.Count;
		}

		//Getters and setters
		public void setMatrix(double[] m) //Changes the vector to m
		{
			int count = 0; //Count of which iteration we're on

			foreach (double val in m) { //Wanted to try a different programming style here!
				this[count] = m[count];
				count++;
			}
		}

		public double[] getVector() //Returns the vector version of the matrix
		{
			double[] result = new double[height];

			for (int i = 0; i < height; i++) {
				result[i] = this[i];
			}

			return result;
		}

		public List<Double> getVectorList() //Returns the vector version of the matrix in list format
		{
			int count = 0; //Count of which iteration we're on
			List<Double> result = new List<Double>();

			for (int i = 0; i < height; i++) {
				result.Add(this[i]);
			}

			return result;
		}

		public void setValue(int l, double v) //Sets the value for a specific compartment of the vector
		{
			if (l >= height) { //Value is out of bounds of vector
				throw new InvalidOperationException("Invalid value set for matrix.");
			}

			matrix [l, 0] = v;
		}

		public double getValue(int l) //Gets values in specified compartment
		{
			if (l >= height) { //Value is out of bounds of vector 
				throw new InvalidOperationException("Invalid value get for matrix.");
			}

			return matrix [l, 0];
		}



		//Indexer
		public double this[int i]
		{
			get {
				return getValue (i);
			}
			set {
				setValue (i, value);
			}
		}


		//Vector operations
		public static Vector operator +(Vector v1, Vector v2) //Vector addition- same concept as matrix addition!!
		{
			Vector answer_list = new Vector (v1.height);
			for (int i = 0; i < v1.height; i++) {
				answer_list.setValue (i, v1 [i] + v2 [i]);
			}

			return answer_list;
		}

		public static double operator *(Vector v1, Vector v2) //Vector dot product: multiply two vectors and obtain a third!
		{
			double answer = 0;
			for (int i = 0; i < v1.height; i++) {
				answer += v1 [i] * v2 [i];
			}

			return answer;
		}

		public static Vector operator *(Scalar sc, Vector v) //Scalar multiplication
		{
			for (int i = 0; i < v.height; i++) {
				v [i] *= sc.value;
			}

			return v;
		}

		public static Vector operator *(Vector v, Scalar sc) //Scalar multiplication
		{
			for (int i = 0; i < v.height; i++) {
				v [i] *= sc.value;
			}

			return v;
		}

		public static Vector operator *(double sc, Vector v) //Scalar multiplication (with double)
		{
			for (int i = 0; i < v.height; i++) {
				v [i] *= sc;
			}

			return v;
		}

		public static Vector operator *(Vector v, double sc) //Scalar multiplication (with double)
		{
			for (int i = 0; i < v.height; i++) {
				v [i] *= sc;
			}

			return v;
		}

		public double getNorm() //Returns the magnitude of the vector formed by these components
		{
			double norm = 0;

			//Norm = square root of sum of all elements in vector squared individually (a-la-Pythagorian theorum)
			for (int i = 0; i < matrix.Length; i++) {
				norm += Math.Pow (matrix [i, 0], 2);
			}

			norm = Math.Round(Math.Sqrt (norm), 5); //The norm is rounded to five decimal places to prevent egregocity!

			return norm;
		}


		//Vectors to strings
/*		public static Vector stringToVector(string vector) //Parses a vector string into a list.  Decided to write my own integer parsing algorithm to get a good feel on what to do
		{
			List<double> list = new List<double> (1);
			bool isDecimal = false; //Tells if you're currently parsing a decimal
			bool isNegative = false; //Tells if you're currently parsing a negative
			double temp = 0; //Stores the current int here.
			double dec = 0; //This is the current decimal power here

			foreach (char c in vector)
			{

				if (c == ',' || c == '}') { //It's a comma/end of vector, so we store the int into the list!
					if (isNegative) {
						temp *= -1; //If is negative, we divide by -1
						isNegative = false;
					}

					list.Add (temp);
					isDecimal = false;
					temp = 0;
					dec = 0;
				} 
				else if (Matrix.isInteger(c)) { //It's an integer!  We add it as a digit to temp.
					double convertedChar = Char.GetNumericValue(c); //c converted into an double
					if (isDecimal) { //Take into account the fact this is a decimal
						dec--;
						temp += convertedChar * Math.Pow (10, dec);

					}
					//If this is not a decimal
					else {
						temp *= 10;
						temp += convertedChar;
					}
				} 
				else if (c == '.') { //It's a decimal!  Let's turn on decimal parse mode!
					isDecimal = true;
				} 
				else if (c == '-') { //It's a negative sign! Convert the int to negative
					isNegative = true;
				}
			}

			return new Vector(list);
		}

		public static string vectorToString( Vector list_vector) //Parses a list to a vector.
		{
			string vector = "{";

			List<Double> list = list_vector.getVectorList();

			for (int i=0; i<list.Count; i++) {
				vector += list[i];
				if (i != list.Count - 1) //Not the last element, add a comma
				{
					vector += ",";
				}
			}

			vector += "}";
			return vector;
		}*/

		public static string getNorm( Vector vector ) //Takes a string of a vector and turn it into a scalar magnitude- the "string" variant
		{
			double norm = 0;

			//Norm = square root of sum of all elements in vector squared individually (a-la-Pythagorian theorum)
			for (int i = 0; i < vector.height; i++) {
				norm += Math.Pow (vector [i], 2);
			}

			norm = Math.Round(Math.Sqrt (norm), 5); //The norm is rounded to five decimal places to prevent egregocity!

			return "" + norm;
		}

		public override bool isVector ()
		{
			return true;
		}

		public override Object getValue()
		{
			return null;
		}
	}
}

