package md50197b187b8fc934de3e002d6de4dc3a4;


public class RegisterNewUserActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ApplikasjonBoknaden.Droid.RegisterNewUserActivity, ApplikasjonBoknaden.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", RegisterNewUserActivity.class, __md_methods);
	}


	public RegisterNewUserActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == RegisterNewUserActivity.class)
			mono.android.TypeManager.Activate ("ApplikasjonBoknaden.Droid.RegisterNewUserActivity, ApplikasjonBoknaden.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
