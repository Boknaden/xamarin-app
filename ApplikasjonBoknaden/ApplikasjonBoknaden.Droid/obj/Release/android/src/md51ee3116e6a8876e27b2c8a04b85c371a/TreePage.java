package md51ee3116e6a8876e27b2c8a04b85c371a;


public class TreePage
	extends android.app.Fragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ApplikasjonBoknaden.Droid.ViewPageExpanders.ApplikasjonBoknaden.Droid.ViewPageExpanders.TreePage, ApplikasjonBoknaden.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TreePage.class, __md_methods);
	}


	public TreePage () throws java.lang.Throwable
	{
		super ();
		if (getClass () == TreePage.class)
			mono.android.TypeManager.Activate ("ApplikasjonBoknaden.Droid.ViewPageExpanders.ApplikasjonBoknaden.Droid.ViewPageExpanders.TreePage, ApplikasjonBoknaden.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
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
