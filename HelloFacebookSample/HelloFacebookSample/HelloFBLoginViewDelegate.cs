using System;
using System.Drawing;
using MonoTouch.FacebookConnect;
using MonoTouch.Foundation;

namespace HelloFacebookSample
{
	public class HelloFBLoginViewDelegate : FBLoginViewDelegate
	{
		HelloFacebookSampleViewController parentViewController;

		public HelloFBLoginViewDelegate(HelloFacebookSampleViewController parentViewController)
		{
			this.parentViewController = parentViewController;
		}

		public override void LoginViewShowingLoggedInUser(FBLoginView loginView)
		{
			Console.WriteLine("LoginViewShowingLoggedInUser");
			// first get the buttons set for login mode
			parentViewController.buttonPostPhoto.Enabled = true;
			parentViewController.buttonPostStatus.Enabled = true;
			parentViewController.buttonPickFriends.Enabled = true;
			parentViewController.buttonPickPlace.Enabled = true;
		}

		public override void LoginViewFetchedUserInfo(FBLoginView loginView, FBGraphUser user)
		{
			Console.WriteLine("LoginViewFetchedUserInfo");
			// here we use helper properties of FBGraphUser to dot-through to first_name and
			// id properties of the json response from the server; alternatively we could use
			// NSDictionary methods such as objectForKey to get values from the my json object
			parentViewController.labelFirstName.Text = String.Format(@"Hello {0}!", user.ObjectForKey(new NSString(@"first_name")).ToString());
			// setting the profileID property of the FBProfilePictureView instance
			// causes the control to fetch and display the profile picture for the user
			parentViewController.profilePic.ProfileID = user.ObjectForKey(new NSString(@"id")).ToString();
			parentViewController.loggedInUser = user;
		}

		public override void LoginViewShowingLoggedOutUser(FBLoginView loginView)
		{
			Console.WriteLine("LoginViewShowingLoggedOutUser");
			parentViewController.buttonPostPhoto.Enabled = false;
			parentViewController.buttonPostStatus.Enabled = false;
			parentViewController.buttonPickFriends.Enabled = false;
			parentViewController.buttonPickPlace.Enabled = false;
			
			parentViewController.profilePic.ProfileID = String.Empty;            
			parentViewController.labelFirstName.Text = String.Empty;
		}
	}
}

