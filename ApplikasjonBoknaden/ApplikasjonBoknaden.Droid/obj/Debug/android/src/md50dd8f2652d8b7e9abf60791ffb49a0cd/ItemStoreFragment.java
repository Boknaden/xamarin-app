package md50dd8f2652d8b7e9abf60791ffb49a0cd;


public class ItemStoreFragment
	extends md50dd8f2652d8b7e9abf60791ffb49a0cd.CostumFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ApplikasjonBoknaden.Droid.DialogFragments.ItemStoreFragment, ApplikasjonBoknaden.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ItemStoreFragment.class, __md_methods);
	}


	public ItemStoreFragment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ItemStoreFragment.class)
			mono.android.TypeManager.Activate ("ApplikasjonBoknaden.Droid.DialogFragments.ItemStoreFragment, ApplikasjonBoknaden.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
