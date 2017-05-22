package md55a4774da55afb1f38f34ae5708d3cfcc;


public class PageFrag
	extends android.app.Fragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ApplikasjonBoknaden.Droid.ViewPageExpanders.PageFrag, ApplikasjonBoknaden.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PageFrag.class, __md_methods);
	}


	public PageFrag () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PageFrag.class)
			mono.android.TypeManager.Activate ("ApplikasjonBoknaden.Droid.ViewPageExpanders.PageFrag, ApplikasjonBoknaden.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
