using UnityEngine;
using System.Collections;
using System;

public class LocalPush : MonoBehaviour {

	public void RegisterForNotif(){
		
		UnityEngine.iOS.NotificationServices.RegisterForNotifications(UnityEngine.iOS.NotificationType.Alert | UnityEngine.iOS.NotificationType.Badge | UnityEngine.iOS.NotificationType.Sound);
	}


	// schedule notification to be delivered in 24 hours
	public void ScheduleNotification(){

		UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();

//		notif.fireDate = DateTime.Now.AddHours(24);
		notif.fireDate = DateTime.Now.AddSeconds(6);

		notif.alertBody = "Cybirdの目覚ましです。早く起きなさい！";

//		notif.soundName = UnityEngine.iOS.LocalNotification.defaultSoundName;
		notif.soundName = "guruguru.wav";

		UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(notif);
	}


	void OnApplicationPause (bool isPause)
	{
		if( isPause ) // App going to background
		{
			// cancel all notifications first.
			#if UNITY_IOS

			UnityEngine.iOS.NotificationServices.ClearLocalNotifications();

			UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications();

			ScheduleNotification ();

			#endif
		}
		else {
			#if UNITY_IOS

			Debug.Log("Local notification count = " + UnityEngine.iOS.NotificationServices.localNotificationCount);

			if (UnityEngine.iOS.NotificationServices.localNotificationCount > 0) {
				Debug.Log(UnityEngine.iOS.NotificationServices.localNotifications[0].alertBody);
			}

			// cancel all notifications first.
			UnityEngine.iOS.NotificationServices.ClearLocalNotifications();

			UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications();

			#endif
		}
	}
}
