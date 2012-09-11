using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using MonoTouch.FacebookConnect;
using MonoTouch.CoreLocation;
using System.Text;
using MonoTouch.ObjCRuntime;

namespace HelloFacebookSample
{
	public partial class HelloFacebookSampleViewController : UIViewController
	{
		public FBGraphUser loggedInUser; 

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public HelloFacebookSampleViewController()
			: base (UserInterfaceIdiomIsPhone ? "HelloFacebookSampleViewController_iPhone" : "HelloFacebookSampleViewController_iPad", null)
		{
		}
		
		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();

			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();


			// Create Login View so that the app will be granted "status_update" permission.
			FBLoginView loginview = new FBLoginView(NSArray.FromNSObjects(new NSString(@"publish_actions")));
			loginview.Frame = new RectangleF(5, 5, loginview.Frame.Width, loginview.Frame.Height);
			loginview.Delegate = new HelloFBLoginViewDelegate(this);

			View.AddSubview(loginview);


			loginview.SizeToFit();
			// Perform any additional setup after loading the view, typically from a nib.
		}
		
		public override void ViewDidUnload()
		{
			base.ViewDidUnload();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets();
		}

		// Post Status Update button handler
		partial void postStatusUpdateClick(NSObject sender)
		{
			Console.WriteLine("postStatusUpdateClick");

			// Post a status update to the user's feed via the Graph API, and display an alert view 
			// with the results or an error.
			string message = string.Format(@"Updating {0}'s status at {1}", loggedInUser.ObjectForKey(new NSString("first_name")), DateTime.Now);

			FBRequestConnection.StartForPostStatusUpdate(message, delegate(FBRequestConnection connection, NSObject result, NSError error) {
				showAlert(message, result, error);
				buttonPostStatus.Enabled = true;
			});
			buttonPostStatus.Enabled = false;
		}


		// Pick Friends button handler
		partial void pickFriendsClick(NSObject sender)
		{
			Console.WriteLine("pickFriendsClick");
			FBFriendPickerViewController friendPickerController = new FBFriendPickerViewController();
			friendPickerController.Title = @"Pick Friends";
			friendPickerController.LoadData();
			friendPickerController.PresentModally(this, true, delegate(FBViewController sender2, bool donePressed) {
				if (!donePressed) {
					return;
				}
				string message;
				
				if (friendPickerController.Selection.Count == 0)
				{
					message = @"<No Friends Selected>";
				}
				else
				{
					//string text = "";
					StringBuilder text = new StringBuilder();
					// we pick up the users from the selection, and create a string that we use to update the text view
					// at the bottom of the display; note that self.selection is a property inherited from our base class
					for (uint i = 0; i <  friendPickerController.Selection.Count; ++i)
					{
						if (text.Length > 0)
						{
							text.Append(@", ");
						}

						FBGraphObject user = (FBGraphObject)Runtime.GetNSObject(friendPickerController.Selection.ValueAt(i));

						text.Append(user.ObjectForKey(new NSString("name")).ToString());
					}
					message = text.ToString();
				}

				UIAlertView alert = new UIAlertView("You Picked:", message, null, "Ok", null);
				alert.Show();
			});
		}

		// Post Photo button handler
		partial void postPhotoClick(NSObject sender)
		{
			Console.WriteLine("postPhotoClick");

			// Just use the icon image from the application itself.  A real app would have a more 
			// useful way to get an image.
			UIImage img = UIImage.FromFile("Icon-72@2x.png");
			FBRequestConnection.StartForUploadPhoto(img, delegate(FBRequestConnection connection, NSObject result, NSError error) {
				showAlert(@"Photo Post", result, error);
				buttonPostPhoto.Enabled = true;
			});
			buttonPostPhoto.Enabled = false;
		}

		// Pick Place button handler
		partial void pickPlaceClick(NSObject sender)
		{
			Console.WriteLine("pickPlaceClick");
			FBPlacePickerViewController placePickerController = new FBPlacePickerViewController();
			placePickerController.Title = @"Pick a Seattle Place";
			placePickerController.LocationCoordinate = new CLLocationCoordinate2D(47.6097, -122.3331);
			placePickerController.LoadData();
			placePickerController.PresentModally(this, true, delegate(FBViewController sender2, bool donePressed) {
				if (!donePressed)
				{
					return;
				}


				//FBGraphObject place = placePickerController.Selection.ObjectForKey(new NSString("name")).ToString()
				UIAlertView alert = new UIAlertView(@"You Picked:", placePickerController.Selection.ObjectForKey(new NSString("name")).ToString(), null, @"Ok", null);
				alert.Show();
			});

		}

		
		// UIAlertView helper for post buttons
		private void showAlert(string message, NSObject result, NSError error)
		{		
			string alertMsg;
			string alertTitle;
			if (error != null)
			{
				alertMsg = error.LocalizedDescription;
				alertTitle = @"Error";
			}
			else
			{
				FBGraphObject resultDict = result as FBGraphObject;

				alertMsg = string.Format(@"Successfully posted '{0}'.\nPost ID: {1}", message, resultDict.ObjectForKey(new NSString(@"id")).ToString());
				alertTitle = @"Success";
			}
			
			UIAlertView alertView = new UIAlertView(alertTitle, alertMsg, null, @"Ok", null);
			alertView.Show();
		}



		public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			if (UserInterfaceIdiomIsPhone)
			{
				return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
			} 
			else
			{
				return true;
			}
		}
	}
}

