package md50197b187b8fc934de3e002d6de4dc3a4;


public class UserPageActivity
	extends md50dd8f2652d8b7e9abf60791ffb49a0cd.CostumFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("ApplikasjonBoknaden.Droid.UserPageActivity, ApplikasjonBoknaden.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", UserPageActivity.class, __md_methods);
	}


	public UserPageActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == UserPageActivity.class)
			mono.android.TypeManager.Activate ("ApplikasjonBoknaden.Droid.UserPageActivity, ApplikasjonBoknaden.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
