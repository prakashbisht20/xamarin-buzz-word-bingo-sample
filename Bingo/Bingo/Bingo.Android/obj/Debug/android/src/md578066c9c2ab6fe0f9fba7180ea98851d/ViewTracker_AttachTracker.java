package md578066c9c2ab6fe0f9fba7180ea98851d;


public class ViewTracker_AttachTracker
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnAttachStateChangeListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onViewAttachedToWindow:(Landroid/view/View;)V:GetOnViewAttachedToWindow_Landroid_view_View_Handler:Android.Views.View/IOnAttachStateChangeListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onViewDetachedFromWindow:(Landroid/view/View;)V:GetOnViewDetachedFromWindow_Landroid_view_View_Handler:Android.Views.View/IOnAttachStateChangeListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Forms.Platform.Android.ViewTracker/AttachTracker, Xamarin.Forms.Platform.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ViewTracker_AttachTracker.class, __md_methods);
	}


	public ViewTracker_AttachTracker () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ViewTracker_AttachTracker.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.Platform.Android.ViewTracker/AttachTracker, Xamarin.Forms.Platform.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onViewAttachedToWindow (android.view.View p0)
	{
		n_onViewAttachedToWindow (p0);
	}

	private native void n_onViewAttachedToWindow (android.view.View p0);


	public void onViewDetachedFromWindow (android.view.View p0)
	{
		n_onViewDetachedFromWindow (p0);
	}

	private native void n_onViewDetachedFromWindow (android.view.View p0);

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
