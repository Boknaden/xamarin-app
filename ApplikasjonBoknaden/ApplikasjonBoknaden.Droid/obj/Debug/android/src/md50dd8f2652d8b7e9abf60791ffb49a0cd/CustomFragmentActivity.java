package md50dd8f2652d8b7e9abf60791ffb49a0cd;


public class CustomFragmentActivity
	extends android.support.v4.app.FragmentActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onActivityResult:(IILandroid/content/Intent;)V:GetOnActivityResult_IILandroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("ApplikasjonBoknaden.Droid.DialogFragments.CustomFragmentActivity, ApplikasjonBoknaden.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CustomFragmentActivity.class, __md_methods);
	}


	public CustomFragmentActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CustomFragmentActivity.class)
			mono.android.TypeManager.Activate ("ApplikasjonBoknaden.Droid.DialogFragments.CustomFragmentActivity, ApplikasjonBoknaden.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onActivityResult (int p0, int p1, android.content.Intent p2)
	{
		n_onActivityResult (p0, p1, p2);
	}

	private native void n_onActivityResult (int p0, int p1, android.content.Intent p2);

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
