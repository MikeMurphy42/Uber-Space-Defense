using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class LevelPlay : MonoBehaviour
{

    public LevelSelector levelSelector;
        
    // Start is called before the first frame update
    void Start()
    {
        IronSource.Agent.init ("1885bb835");
        IronSource.Agent.validateIntegration();
    }

    private void OnEnable()
    {
        // BannerAD Events
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
        
        IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
        IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;        
        IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent; 
        IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent; 
        IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
        IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;
        
        //Add AdInfo Banner Events
        IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;
        
        // Interstitial AD Events
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;        
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent; 
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent; 
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
            
        //Add AdInfo Interstitial Events
        IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
        IronSourceInterstitialEvents.onAdLoadFailedEvent += InterstitialOnAdLoadFailed;
        IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialOnAdOpenedEvent;
        IronSourceInterstitialEvents.onAdClickedEvent += InterstitialOnAdClickedEvent;
        IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
        IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialOnAdShowFailedEvent;
        IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;
        
        
        
        // RewardedAD Events
        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent; 
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent; 
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
        
        
        //Add AdInfo Rewarded Video Events
        //Add AdInfo Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;

        
        
        
    }

    void OnApplicationPause(bool isPaused) {                 
        IronSource.Agent.onApplicationPause(isPaused);
    }
    // Banner Buttons
    public void LoadBanner()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
    }

    public void DestroyBanner()
    {
        IronSource.Agent.destroyBanner();
    }
    // FullSize Buttons
    
    public void LoadFullAD()
    {
        IronSource.Agent.loadInterstitial();
    }

    //public void InterstitialAdReadyEvent(){}

    public void ShowFullSizeAD()
    {
        if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial();
        }
        else
        {
            Debug.Log(" AD Is Not Ready!!! ");
        }
    }
    
    // Rewarded Buttons

    public void ShowRewardedAd()
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            Debug.Log(" Rewarded AD Is Not Ready!!! ");
        }
    }
    
    
    private void SdkInitializationCompletedEvent(){  }
    
    //Invoked once the banner has loaded
    void BannerAdLoadedEvent() {
    }
//Invoked when the banner loading process has failed.
//@param description - string - contains information about the failure.
    void BannerAdLoadFailedEvent (IronSourceError error) {
    }
// Invoked when end user clicks on the banner ad
    void BannerAdClickedEvent () {
    }
//Notifies the presentation of a full screen content following user click
    void BannerAdScreenPresentedEvent () {
    }
//Notifies the presented screen has been dismissed
    void BannerAdScreenDismissedEvent() {
    }
//Invoked when the user leaves the app
    void BannerAdLeftApplicationEvent() {
    }


    /************* Banner AdInfo Delegates *************/
//Invoked once the banner has loaded
    void BannerOnAdLoadedEvent(IronSourceAdInfo adInfo) 
    {
    }
//Invoked when the banner loading process has failed.
    void BannerOnAdLoadFailedEvent(IronSourceError ironSourceError) 
    {
    }
// Invoked when end user clicks on the banner ad
    void BannerOnAdClickedEvent(IronSourceAdInfo adInfo) 
    {
    }
//Notifies the presentation of a full screen content following user click
    void BannerOnAdScreenPresentedEvent(IronSourceAdInfo adInfo) 
    {
    }
//Notifies the presented screen has been dismissed
    void BannerOnAdScreenDismissedEvent(IronSourceAdInfo adInfo) 
    {
    }
//Invoked when the user leaves the app
    void BannerOnAdLeftApplicationEvent(IronSourceAdInfo adInfo) 
    {
    }
    // Banner callbacks
    // Invoked when the initialization process has failed.
// @param description - string - contains information about the failure.
    void InterstitialAdLoadFailedEvent (IronSourceError error) {
    }
// Invoked when the ad fails to show.
// @param description - string - contains information about the failure.
    void InterstitialAdShowFailedEvent(IronSourceError error) {
    }
// Invoked when end user clicked on the interstitial ad
    void InterstitialAdClickedEvent () {
    }
// Invoked when the interstitial ad closed and the user goes back to the application screen.
    void InterstitialAdClosedEvent () {
    }
// Invoked when the Interstitial is Ready to shown after load function is called
    void InterstitialAdReadyEvent() {
    }
// Invoked when the Interstitial Ad Unit has opened
    void InterstitialAdOpenedEvent() {
    }
// Invoked right before the Interstitial screen is about to open.
// NOTE - This event is available only for some of the networks. 
// You should not treat this event as an interstitial impression, but rather use InterstitialAdOpenedEvent
    void InterstitialAdShowSucceededEvent() 
    {
    }
    
    /************* Interstitial AdInfo Delegates *************/
// Invoked when the interstitial ad was loaded succesfully.
    void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo) {
    }
// Invoked when the initialization process has failed.
    void InterstitialOnAdLoadFailed(IronSourceError ironSourceError) {
    }
// Invoked when the Interstitial Ad Unit has opened. This is the impression indication. 
    void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo) {
    }
// Invoked when end user clicked on the interstitial ad
    void InterstitialOnAdClickedEvent(IronSourceAdInfo adInfo) {
    }
// Invoked when the ad failed to show.
    void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo) {
    }
// Invoked when the interstitial ad closed and the user went back to the application screen.
    void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo) {
    }
// Invoked before the interstitial ad was opened, and before the InterstitialOnAdOpenedEvent is reported.
// This callback is not supported by all networks, and we recommend using it only if  
// it's supported by all networks you included in your build. 
    void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo) 
    {
    }
    
    //Full Size Callbacks
    
    
    //Rewarded
     //Invoked when the RewardedVideo ad view has opened.
  //Your Activity will lose focus. Please avoid performing heavy 
  //tasks till the video ad will be closed.
  void RewardedVideoAdOpenedEvent() {
  }  
  //Invoked when the RewardedVideo ad view is about to be closed.
  //Your activity will now regain its focus.
  void RewardedVideoAdClosedEvent() {
  }
  //Invoked when there is a change in the ad availability status.
  //@param - available - value will change to true when rewarded videos are available. 
  //You can then show the video by calling showRewardedVideo().
  //Value will change to false when no videos are available.
  void RewardedVideoAvailabilityChangedEvent(bool available) {
    //Change the in-app 'Traffic Driver' state according to availability.
    bool rewardedVideoAvailability = available;
  }

  
  
  
  
  
  
  
  
  
  
  
  
  //Invoked when the user completed the video and should be rewarded. 
  
  //If using server-to-server callbacks you may ignore this events and wait for 
  // the callback from the  ironSource server.
  //@param - placement - placement object which contains the reward data
  
  
  void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
  {

      //levelSelector.RestartLevel();
      //Debug.Log(" ######## Reward Player With Respawn ######## ");

  }
  
  
  
  
  
  
  
  
  
  
  
  
  
  //Invoked when the Rewarded Video failed to show
  //@param description - string - contains information about the failure.
  void RewardedVideoAdShowFailedEvent (IronSourceError error){
  }

  // ----------------------------------------------------------------------------------------
  // Note: the events below are not available for all supported rewarded video ad networks. 
  // Check which events are available per ad network you choose to include in your build. 
  // We recommend only using events which register to ALL ad networks you include in your build. 
  // ----------------------------------------------------------------------------------------

  //Invoked when the video ad starts playing. 
  void RewardedVideoAdStartedEvent() { 
  } 
  //Invoked when the video ad finishes playing. 
  void RewardedVideoAdEndedEvent() { 
  }
  //Invoked when the video ad is clicked. 
  void RewardedVideoAdClickedEvent(IronSourcePlacement placement) { 
  }


    
  
  // INFO
  
/************* RewardedVideo AdInfo Delegates *************/
// Indicates that there’s an available ad.
// The adInfo object includes information about the ad that was loaded successfully
// This replaces the RewardedVideoAvailabilityChangedEvent(true) event
  void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo) {
  }
// Indicates that no ads are available to be displayed
// This replaces the RewardedVideoAvailabilityChangedEvent(false) event
  void RewardedVideoOnAdUnavailable() {
  }
// The Rewarded Video ad view has opened. Your activity will loose focus.
  void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo){
  }
// The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
  void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
  {
      
      levelSelector.RestartLevel();
      
  }
  
  
  
  
// The user completed to watch the video, and should be rewarded.
// The placement parameter will include the reward data.
// When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
  void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo){
  }
// The rewarded video ad was failed to show.
  void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo){
  }
// Invoked when the video ad was clicked.
// This callback is not supported by all networks, and we recommend using it only if
// it’s supported by all networks you included in your build.
  void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo){
  }

}
