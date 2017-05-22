package md55a4774da55afb1f38f34ae5708d3cfcc;


public class AdPackAdapter
	extends md55a4774da55afb1f38f34ae5708d3cfcc.CustomPageAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getPageTitle:(I)Ljava/lang/CharSequence;:GetGetPageTitle_IHandler\n" +
			"n_getCount:()I:GetGetCountHandler\n" +
			"n_instantiateItem:(Landroid/view/ViewGroup;I)Ljava/lang/Object;:GetInstantiateItem_Landroid_view_ViewGroup_IHandler\n" +
			"";
		mono.android.Runtime.register ("ApplikasjonBoknaden.Droid.ViewPageExpanders.AdPackAdapter, ApplikasjonBoknaden.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AdPackAdapter.class, __md_methods);
	}


	public AdPackAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == AdPackAdapter.class)
			mono.android.TypeManager.Activate ("ApplikasjonBoknaden.Droid.ViewPageExpanders.AdPackAdapter, ApplikasjonBoknaden.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public AdPackAdapter (android.content.Context p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == AdPackAdapter.class)
			mono.android.TypeManager.Activate ("ApplikasjonBoknaden.Droid.ViewPageExpanders.AdPackAdapter, ApplikasjonBoknaden.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public java.lang.CharSequence getPageTitle (int p0)
	{
		return n_getPageTitle (p0);
	}

	private native java.lang.CharSequence n_getPageTitle (int p0);


	public int getCount ()
	{
		return n_getCount ();
	}

	private native int n_getCount ();


	public java.lang.Object instantiateItem (android.view.ViewGroup p0, int p1)
	{
		return n_instantiateItem (p0, p1);
	}

	private native java.lang.Object n_instantiateItem (android.view.ViewGroup p0, int p1);

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
