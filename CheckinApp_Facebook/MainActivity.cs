using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace CheckinApp_Facebook
{
	[Activity (Label = "CheckinApp Facebook", MainLauncher = true, Icon = "@drawable/icon", Theme="@android:style/Theme.Holo.Light")]
	public class MainActivity : Activity
	{
		private string token = null;
		private Button buttonPost;
		private EditText editTextMessage;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			var sharedPreferences = GetSharedPreferences ("CheckinAppPreferences", FileCreationMode.WorldReadable);

			Intent intent = new Intent (this, typeof(AuthActivity));
			StartActivityForResult (intent, 1);

			buttonPost = FindViewById<Button> (Resource.Id.buttonPost);
			editTextMessage = FindViewById<EditText> (Resource.Id.editTextMessage);
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			token = data.GetStringExtra ("token");

			Console.WriteLine ("Token: " + token);

			buttonPost.Click += async delegate {
				Console.WriteLine(token);
				Console.WriteLine(editTextMessage.Text);

				Facebook facebookClient = new Facebook("1492339931014967", "7ae094df0f071a1972ed7c7354943f9a");
				facebookClient.UserToken = token;

				string response = await facebookClient.publishFeed(editTextMessage.Text) as string;

				Console.WriteLine(response);
			};

			base.OnActivityResult (requestCode, resultCode, data);
		}
	}
}


