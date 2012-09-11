//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace HelloFacebookSample
{
	[Register ("HelloFacebookSampleViewController")]
	partial class HelloFacebookSampleViewController
	{
		[Outlet]
		public MonoTouch.UIKit.UIButton buttonPickFriends { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UIButton buttonPickPlace { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UIButton buttonPostStatus { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UIButton buttonPostPhoto { get; set; }

		[Outlet]
		public MonoTouch.FacebookConnect.FBProfilePictureView profilePic { get; set; }
	
		[Outlet]
		public MonoTouch.UIKit.UILabel labelFirstName { get; set; }


	




		[Action ("postStatusUpdateClick:")]
		partial void postStatusUpdateClick(MonoTouch.Foundation.NSObject sender);

		[Action ("pickFriendsClick:")]
		partial void pickFriendsClick(MonoTouch.Foundation.NSObject sender);

		[Action ("postPhotoClick:")]
		partial void postPhotoClick(MonoTouch.Foundation.NSObject sender);

		[Action ("pickPlaceClick:")]
		partial void pickPlaceClick(MonoTouch.Foundation.NSObject sender);


		void ReleaseDesignerOutlets()
		{
		}
	}
}
