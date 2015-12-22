package md5d63e4ae6db50edb7a8f8d0b15173a3c9;


public class MatrixOperationsActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("LinearAlgebraApp.MatrixOperationsActivity, LinearAlgebraApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MatrixOperationsActivity.class, __md_methods);
	}


	public MatrixOperationsActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MatrixOperationsActivity.class)
			mono.android.TypeManager.Activate ("LinearAlgebraApp.MatrixOperationsActivity, LinearAlgebraApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
