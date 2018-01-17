using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class FacebookController : MonoBehaviour {

	void Awake ()
	{
		InitApp ();	
	}

	void OnApplicationPause (bool pauseStatus)
	{
		if (!pauseStatus) {
			InitApp();
		}
	}

	private void InitApp () 
	{
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	public void Share() {
		FB.ShareLink (
			new Uri ("https://www.facebook.com/games/?app_id=175548269720146"),
			callback: ShareCallback
		);
	}

	private void ShareCallback (IShareResult result) {
		Dictionary<string, object> shareEventParams = new Dictionary<string, object> ();

		if (result.Cancelled) {
			shareEventParams ["outcome"] = "Cancelled";
		} else if (!String.IsNullOrEmpty (result.Error)) {
			shareEventParams ["outcome"] = "Error";
		} else if (!String.IsNullOrEmpty(result.PostId)) {				
			shareEventParams ["outcome"] = "SuccessWithPostId";
		} else {
			shareEventParams ["outcome"] = "Success";
		}

		FB.LogAppEvent ("Share", parameters: shareEventParams);
	}
}
