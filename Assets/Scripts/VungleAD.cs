using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VungleAD: MonoBehaviour
{
    string appID = "";

    string windowsAppID = "622a5a1a1d9f2a321150c32b";

    string placementADID = "";

    [SerializeField] public Button playAd;


    private void Awake()
    {


#if UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
        appID = windowsAppID;
#endif

#if UNITY_IPHONE
    placementID = "ios_placement_id";
#elif UNITY_ANDROID
    placementID = "android_placement_id";
#elif UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
        placementADID = "DEFAULT-3301631";
#endif

        initializeEventHandlers();
        Vungle.init(appID);
        Vungle.loadAd(placementADID);
        playAd.interactable = false;
    }

    // Start is called before the first frame update
    void Start()
    {
   //     playAd.onClick.AddListener(onPlayPlacement3);
    }

    public void onPlayPlacement3()
    {
        // option to customize alert window and send user_id
        Dictionary<string, object> options = new Dictionary<string, object>();
        options["userTag"] = "test_user_id";
        options["alertTitle"] = "Careful!";
        options["alertText"] = "If the video isn't completed you won't get your reward! Are you sure you want to close early?";
        options["closeText"] = "Close";
        options["continueText"] = "Keep Watching";

        Vungle.playAd(placementADID);
    }

    void initializeEventHandlers()
    {
        Vungle.onAdStartedEvent += (placementID) =>
        {
            Debug.Log("Ad " + placementID + " is starting!  Pause your game  animation or sound here.");
            EventBroker.CallPlayAd();
        };

        Vungle.onAdFinishedEvent += (placementID, args) =>
        {
            EventBroker.CallExtraLife();
            Debug.Log("Ad finished - placementID " + placementID + ", was call to action clicked:" + args.WasCallToActionClicked + ", is completed view:"
                      + args.IsCompletedView);
        };

        Vungle.adPlayableEvent += (placementID, adPlayable) =>
        {
            Debug.Log("Ad's playable state has been changed! placementID " + placementID + ". Now: " + adPlayable);
            if (placementID == placementADID)
            {
                playAd.interactable = true;
            }
            
        };

        Vungle.onInitializeEvent += () =>
        {
            Debug.Log("SDK initialized");

        };

        //For iOS and Android only
        Vungle.onAdClickEvent += (placementID) =>
        {
            EventBroker.CallExtraLife();
            Debug.Log("onClick - Log: " + placementID);
        };

        //For iOS and Android only
        Vungle.onAdRewardedEvent += (placementID) =>
        {
            EventBroker.CallExtraLife();
            Debug.Log("onAdRewardedEvent - Log: " + placementID);
        };

        //For iOS and Android only
        Vungle.onAdEndEvent += (placementID) =>
        {
            EventBroker.CallExtraLife();
            Debug.Log("onAdEnd - Log: " + placementID);
        };
    }
}
