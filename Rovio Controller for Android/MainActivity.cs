using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Rovio;
using Android.Content.PM;

namespace Rovio_Controller_for_Android
{
    [Activity(Label = "Rovio Controller for Android", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button loginButton = FindViewById<Button>(Resource.Id.button1);
            var Rov_ip = FindViewById<EditText>(Resource.Id.editText1);
            var Rov_user = FindViewById<EditText>(Resource.Id.editText2);
            var Rov_password = FindViewById<EditText>(Resource.Id.editText3);

            loginButton.Click += (o, e) => login(Rov_ip.Text, Rov_user.Text, Rov_password.Text);
        }

        void login(String Rov_ip, String Rov_user, String Rov_password)
        {
            string Rovio_ip;
            string username;
            string password;
            Rovio_ip = "http://" + Rov_ip;
            username = Rov_user;
            password = Rov_password;
            string Rovio_streamIP = Rov_ip;
            try {
                Rovio.Robot robot = new Rovio.Robot(Rovio_ip, username, password);
                var activity2 = new Intent(this, typeof(Activity1));
                activity2.PutExtra("IP", Rovio_ip);
                activity2.PutExtra("Stream IP", Rovio_streamIP);
                activity2.PutExtra("Username", username);
                activity2.PutExtra("Password", password);
                StartActivity(activity2);
            }
            catch(Exception e)
            {
                DisplayAlert("Error", "Connection Failed, Rovio may be offline or incorrect login details may of been entered.", "OK");
            }
        }

        private void DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }
    }
}

