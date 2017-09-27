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
using Android.Media;


namespace Alarm
{
    [Activity(Label = "Ring")]
    public class Ring : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Ring);

            Button stopAlarm = FindViewById<Button>(Resource.Id.Stop);
            
            Toast.MakeText(this, "Alarm Ringing at"+DateTime.Now, ToastLength.Short).Show();
            MediaPlayer mp = MediaPlayer.Create(this, Resource.Raw.pizza);
            Vibrator vb = (Vibrator)this.ApplicationContext.GetSystemService(Context.VibratorService);

            mp.Start();
            vb.Vibrate(9000);
            stopAlarm.Click += (sender, e) =>
            {
                vb.Cancel();
                mp.Stop();
                var newintent = new Intent(this, typeof(MainActivity));
                StartActivity(newintent);
                Finish();
            };

            
        }
        
     
    }
}