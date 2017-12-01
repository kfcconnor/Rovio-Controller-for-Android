using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Content.PM;
using Uri = Android.Net.Uri;
using Android.Hardware;

namespace Rovio_Controller_for_Android
{
    [Activity(Label = "Activity1", ScreenOrientation = ScreenOrientation.Landscape)]
    public class Activity1 : Activity
    {
        static readonly object _syncLock = new object();
        SensorManager _sensorManager;
        TextView _sensorTextView;
        Rovio.Robot controlBot;
        Button Forward;
        Button Backward;
        Button Left;
        Button Right;
        Button rotateLeft;
        Button rotateRight;
        VideoView Video;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string IP = Intent.GetStringExtra("IP");
            string StreamIP = Intent.GetStringExtra("Stream IP");
            string username = Intent.GetStringExtra("Username");
            string password = Intent.GetStringExtra("Password");
            
            var MainActivity = new Intent(this, typeof(MainActivity));
            

            try
            {
                
                SetContentView(Resource.Layout.layout1);
                Forward = FindViewById<Button>(Resource.Id.button1);
                Backward = FindViewById<Button>(Resource.Id.button2);
                Left = FindViewById<Button>(Resource.Id.button3);
                Right = FindViewById<Button>(Resource.Id.button4);
                rotateLeft = FindViewById<Button>(Resource.Id.button5);
                rotateRight = FindViewById<Button>(Resource.Id.button6);
                Video = FindViewById<VideoView>(Resource.Id.videoView1);
                controlBot = new Rovio.Robot(IP, username, password);
                _sensorManager = (SensorManager)GetSystemService(Context.SensorService);
                _sensorTextView = FindViewById<TextView>(Resource.Id.accelerometer_text);
                var surfaceOrientation = WindowManager.DefaultDisplay.Rotation;

                update();
            }
            catch (Exception e)
            {
                DisplayAlert("Error", "Connection Failed, Rovio may be offline or incorrect login details may of been entered.", "OK");
                StartActivity(MainActivity);
            }

            
           
        }

        public void update()
        {
            Bitmap image = controlBot.API.Camera.GetImage();
            Forward.Click += (o, e) => ForwardPress();
            Video.SetVideoURI(Uri.Parse("rtsp://192.168.0.1/webcam"));
            Backward.Click += (o, e) => BackwardPress();
            Left.Click += (o, e) => LeftPress();
            Right.Click += (o, e) => RightPress();
            rotateLeft.Click += (o, e) => RotateLeftPress();
            rotateRight.Click += (o, e) => RotateRightPress();
            

        }

        private void DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }

        void ForwardPress()
        {
            for (int i = 5; i <= 20; i++)
            {
                controlBot.Drive.Forward(30);
            }

        }

        void BackwardPress()
        {
            for (int i = 5; i <= 20; i++)
            {
                controlBot.Drive.Backward(30);
            }
        }
        void LeftPress()
        {
            for (int i = 5; i <= 20; i++)
            {
                controlBot.Drive.StraightLeft(30);
            }
        }
        void RightPress()
        {
            for (int i = 5; i <= 20; i++)
            {
                controlBot.Drive.StraightRight(30);
            }
        }
        void RotateLeftPress()
        {
            for (int i = 5; i <= 20; i++)
            {
                controlBot.Drive.RotateLeft(30);
            }
        }
        void RotateRightPress()
        {
            for (int i = 5; i <= 20; i++)
            {
                controlBot.Drive.RotateRight(30);
            }
        }
        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
           
        }

        public void OnSensorChanged(SensorEvent e)
        {
            lock (_syncLock)
            {
                _sensorTextView.Text = string.Format("x={0:f}, y={1:f}, y={2:f}", e.Values[0], e.Values[1], e.Values[2]);
                float y = e.Values[1];
                if(y > 5)
                {
                    RotateLeftPress();
                }
                else if (y < -5)
                {
                    RotateRightPress();
                }
            }
        }
    }
}