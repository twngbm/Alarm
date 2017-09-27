using Android.App;
using Android.Widget;
using Android.OS;
using Android.Systems;
using System.Timers;
using System.Threading;
using System;
using Android.Content;
using Java.Lang;
using Java.Util;

namespace Alarm
{
    [Activity(Label = "Alarm", MainLauncher = true)]

    

    public class MainActivity : Activity
    {
        private TextView time_display;
        //private Button pick_button;

        private int hour;
        private int minute;

        const int TIME_DIALOG_ID = 0;
        private PendingIntent pendingIntent;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Intent alarmIntent = new Intent(this, typeof(Ring));
            pendingIntent = PendingIntent.GetActivity(this, 0, alarmIntent, 0);
            Button setAlarm = FindViewById<Button>(Resource.Id.SetAlarm);
            Button cancelAlarm = FindViewById<Button>(Resource.Id.stopAlarm);
            // Capture our View elements
            time_display = FindViewById<TextView>(Resource.Id.timeDisplay);
            

            // Add a click listener to the button
            setAlarm.Click += (o, e) => ShowDialog(TIME_DIALOG_ID);

            // Get the current time
            hour = DateTime.Now.Hour;
            minute = DateTime.Now.Minute;

            // Display the current date
            UpdateDisplay();
            cancelAlarm.Click += (object sender, EventArgs e) =>
            {
                Cancel();
            };
            
        }
        protected override Dialog OnCreateDialog(int id)
        {
            if (id == TIME_DIALOG_ID)
                return new TimePickerDialog(this, TimePickerCallback, hour, minute, false);

            return null;
        }
        private void UpdateDisplay()
        {
            string time = string.Format("{0}:{1}", hour, minute.ToString().PadLeft(2, '0'));
            time_display.Text = time;
        }
        private void TimePickerCallback(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            

            hour = e.HourOfDay;
            minute = e.Minute;
            UpdateDisplay();
            AlarmManager manager = (AlarmManager)GetSystemService(Context.AlarmService);
            Calendar calendar = Calendar.GetInstance(Java.Util.TimeZone.Default);
            calendar.Set(CalendarField.HourOfDay, hour);
            calendar.Set(CalendarField.Minute, minute);
            calendar.Set(CalendarField.Second, 0);
            Toast.MakeText(this, "Alarm set to" + string.Format("{0}:{1}", hour, minute.ToString().PadLeft(2, '0')), ToastLength.Long).Show();
            
            manager.Set(AlarmType.RtcWakeup, calendar.TimeInMillis, pendingIntent);
        }
        public void Cancel()
        {
            AlarmManager manager = (AlarmManager)GetSystemService(Context.AlarmService);
            manager.Cancel(pendingIntent);
            Toast.MakeText(this, "Alarm Canceled", ToastLength.Long).Show();

        }
       

        

    }
 
[BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {

        public override void OnReceive(Context context, Intent intent)
        {
            
                Toast.MakeText(context, "I'm running ", ToastLength.Short).Show();
        
        }
    }


}

