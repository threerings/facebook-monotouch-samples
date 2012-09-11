using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using MonoTouch.FacebookConnect;

namespace HelloFacebookSample
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		HelloFacebookSampleViewController viewController;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{

			// BUG:
			// Nib files require the type to have been loaded before they can do the
			// wireup successfully.  
			// http://stackoverflow.com/questions/1725881/unknown-class-myclass-in-interface-builder-file-error-at-runtime
			//[FBProfilePictureView class];



			window = new UIWindow(UIScreen.MainScreen.Bounds);
			
			viewController = new HelloFacebookSampleViewController();
			window.RootViewController = viewController;
			window.MakeKeyAndVisible();
			
			return true;
		}

		public override bool HandleOpenURL(UIApplication application, NSUrl url)
		{
			return FBSession.GetActiveSession().HandleOpenUrl(url);
		}

		public override void WillTerminate(UIApplication application)
		{
			// FBSample logic
			// if the app is going away, we close the session object
			FBSession.GetActiveSession().Close();
		}
	}
}

