using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinearAlgebraApp
{
	public class Matrix : Element//Operates with doubles, all matrices are width*height
	{
		public int height{ get; set; } //Number of columns
		public int width{ get; set; } //Number of rows
		protected double[,] matrix; //Do not operate on this directly!

		public static char[] integer = new char[]{'1', '2', '3', '4', '5', '6', '7', '8', '9', '0'};

		public Matrix (int h, int w) //Instantiates a matrix with the specified width and height.
		{
			height = h;
			width = w;
			matrix = new double[h, w];
		}
			
		public Matrix (double[,] m) //Automatically instantiates a matrix
		{
			height = m.GetLength(0);
			width = m.GetLength(1);
			matrix = m;
		}

		//Getters and setters
		public void setMatrix(double[,] m) //Changes the matrix to m
		{
			height = m.GetLength(0);
			width = m.GetLength(1);
			matrix = m;
		}

		public double[,] getMatrix() //Returns the matrix
		{
			return matrix;
		}

		public void setValue(int h, int w, double v) //Sets the value for a specific compartment of the matrix
		{
			if (h >= height || w >= width) { //Value is out of bounds of matrix 
				throw new InvalidOperationException("Invalid value set for matrix.");
			}

			matrix [h, w] = v;
		}

		public double getValue(int h, int w) //Gets values in specified compartment
		{
			if (h >= height || w >= width) { //Value is out of bounds of matrix 
				throw new InvalidOperationException("Invalid value get for matrix.");
			}

			return matrix [h, w];
		}

		//Indexer
		public double this[int i, int j]
		{
			get {
				return getValue (i, j);
			}
			set {
				setValue (i, j, value);
			}
		}


		//Boolean functions
		public bool isSquare() //Tells you if the matrix is square (i.e. if width = height)
		{
			return (height == width);
		}

		public bool HasEqualDimensionsTo(Matrix b) //Tells you if this matrix has same dimensions as other matrix
		{
			return (this.height == b.height && this.width == b.width);
		}


		//Matrix operations
		public static Matrix operator +(Matrix m1, Matrix m2) //Matrix addition: add values of two matrices of same dimensions:
		{
			if ( !(m1.HasEqualDimensionsTo(m2)) ) { //If the dimensions of two vectors are not the same...
				throw new InvalidOperationException("Invalid value set for matrix.");
			}

			double value; //Stores a temporary value equal to sum of values in corresponding cells of both matrices

			Matrix sum = new Matrix (m1.height, m1.width);

			for (int i=0; i<m1.height; i++) //Adding all the respective values of each matrix!
			{
				for (int j = 0; j < m1.width; j++) {
					value = m1.getValue (i, j) + m2.getValue (i, j);
					sum.setValue (i, j, value);
				}
			}

			return sum;
		}

	   public static Matrix operator *(Matrix m1, Matrix m2) //Matrix cartesian product
		{
			if (m1.width != m2.height) {  //Must ensure that width of first matrix is equal that height of second
				throw new InvalidOperationException("Cannot perform cartesian product of matrices- width of m1 does not equal height of m2");
			}

			int newHeight = m1.height; //(height)m x (width)n * (height)n x (width)r = (height)m x (width)r
			int newWidth = m2.width; 
			int size = m1.width;
			double temp = 0; //Where we'll store the variable to put into each matrix compartment.

			Matrix answer = new Matrix (newWidth, newHeight);

			//i = height, j = width
			for (int i = 0; i < newHeight; i++) {
				for (int j = 0; j < newWidth; j++) {

					temp = m1.getRowVector (i) * m2.getColumnVector (j);

					answer [i, j] = temp;
					temp = 0;
				}
			}

			return answer;

		}

		//Row and column vectors
		public Vector getRowVector(int h) //Gets the row at height of h
		{
			if (h >= width) {
				throw new InvalidOperationException("Row vector index greater than height of array");
			}

			Vector vector = new Vector (height);

			for (int i = 0; i < height; i++) {
				vector [i] = this [i, h];
			}

			return vector;
		}

		public Vector getColumnVector(int w) //Gets the column at width of w
		{
			if (w >= height) {
				throw new InvalidOperationException("Row vector index greater than height of array");
			}

			Vector vector = new Vector (width);

			for (int i = 0; i < width; i++) {
				vector [i] = this [w, i];
			}

			return vector;
		}

		public double determinant() //Returns the determinant of this matrix if it's square
		{
			if (! ( isSquare() ) ) { //No determinant if array is not square!
				throw new InvalidOperationException("Cannot perform determinant math- matrix is not square");
			}

			double answer = 0;

			return answer;
		}

		public static bool isInteger(char s) //Checks to see if string is integer
		{
			return integer.Contains (s);
		}

		public override string printElement() //Returns a string for this matrix
		{
			string matrix_string = "";

			//Creating matrix
			for (int i = 0; i < height; i++) { //Each row
				matrix_string += "{";

				for (int j = 0; j < width; j++) { //Each column
					matrix_string += matrix[i, j];

					if (j < width - 1) { //This isn't the last element
						matrix_string += ", ";
					}
				}

				matrix_string += "}";
				if (i != height - 1) { //Line break after each row except the last
					matrix_string += "\n";
				}
			}

			return matrix_string;
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
			return null;
		}

	}
}

