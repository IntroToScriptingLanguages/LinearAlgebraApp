using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace LinearAlgebraApp
{
	[Activity (Label = "LinearAlgebraApp", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button vector = FindViewById<Button> (Resource.Id.vectoroperations);
			Button matrix = FindViewById<Button> (Resource.Id.matrixoperations);


			vector.Click += delegate {
				StartActivity(typeof(VectorOperationsActivity));
			};

			matrix.Click += delegate {
				StartActivity(typeof(MatrixOperationsActivity));
			};
		}
	}
}


