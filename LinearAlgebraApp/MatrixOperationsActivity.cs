
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

namespace LinearAlgebraApp
{
	[Activity (Label = "MatrixOperationsActivity")]			
	public class MatrixOperationsActivity : Activity
	{
		//Variables declared here:
		EditText scalarInput; 
		LinearLayout input;
		TextView output;
		Button newmatrix, determinant, plus, times, clear, menu;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.MatrixOperations);

			// Declare elements here
			newmatrix = FindViewById<Button>(Resource.Id.newmatrix);
			determinant = FindViewById<Button>(Resource.Id.determinant);
			plus = FindViewById<Button>(Resource.Id.plus);
			times = FindViewById<Button>(Resource.Id.times);
			clear = FindViewById<Button>(Resource.Id.clear);
			menu = FindViewById<Button>(Resource.Id.menu);
			input = FindViewById<LinearLayout> (Resource.Id.matrixInput);
			scalarInput = FindViewById<EditText> (Resource.Id.scalarInput);
			output = FindViewById<TextView>(Resource.Id.matrixOutput);

			newmatrix.Click += newMatrix; //NEW MATRIX/INPUT
			clear.Click += clearData; //CLEAR
			menu.Click += returnToMenu; //MENU

		}

		private void newMatrix(object sender, EventArgs ea) //If newmatrix says "NEW MATRIX", make matrixinput visible and swith to "INPUT", if newmatrix says "INPUT", switch to MatrixCreator with data from matrixinput, make input invisible, 
		{
		}

		private void clearData(object sender, EventArgs ea) //Clears everything!
		{
			//Not implemented yet!
		}

		private void returnToMenu(object sender, EventArgs ea) //Returns to the main menu!
		{
			StartActivity (typeof(MainActivity));
		}
	}
}

