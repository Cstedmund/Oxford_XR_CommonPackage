using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DTT.ScreenRotationManagement;
using UnityEngine.Video;
using System;
using UnityEngine.UI;

public static class FunctionLibrary {
    public enum CurrentDevice {
        iPhone,
        iPad,
        Android,
        AndroidTablet,
        OtherDevice
    }

    public static readonly Color sciGreen = ColorInHex("#359A37");
    public static readonly Color sciOrange = ColorInHex("#FF9A37");
    public static readonly Color sciGreyBlue = ColorInHex("#516482");
    public static readonly Color sciBrownYellow = ColorInHex("#BF9000");

    public static readonly Color phyGreen = ColorInHex("#359A37");
    public static readonly Color phyOrange = ColorInHex("#FF9A37");
    public static readonly Color phyGreyBlue = ColorInHex("#516482");
    public static readonly Color phyGreyGreen = ColorInHex("#5D9069");
    public static readonly Color phyGreyRed = ColorInHex("#B44044");
    public static readonly Color phyBrownYellow = ColorInHex("#BF9000");
    public static readonly Color phyGreenBlue = ColorInHex("#00ACB0");
    public static readonly Color phyYellow = ColorInHex("#F19500");

    private static float DeviceDiagonalSizeInInches() {
        float screenWidth = Screen.width / Screen.dpi;
        float screenHeight = Screen.height / Screen.dpi;
        float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth,2) + Mathf.Pow(screenHeight,2));
        Debug.Log("Device Screen Size: " + diagonalInches);
        return diagonalInches;
    }

    public static CurrentDevice GetCurrentDevice() {
#if UNITY_IOS
        var identifier = SystemInfo.deviceModel;
        if(identifier.StartsWith("iPhone")) {
            Debug.Log("Current Devices is iPhone");
            return CurrentDevice.iPhone;
        } else if(identifier.StartsWith("iPad")) {
            Debug.Log("Current Devices is iPad");
            return CurrentDevice.iPad;
        }
#endif

        //#if UNITY_EDITOR
        //        float aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
        //        Debug.Log("Device Aspect ratio:" + aspectRatio);
        //        bool isTablet = (DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f);

        //        if (isTablet) {
        //            Debug.Log("Current Devices is AndroidTablet");
        //            return CurrentDevice.AndroidTablet;
        //        } else {
        //            Debug.Log("Current Devices is Android");
        //            return CurrentDevice.Android;
        //        }
        //#endif

#if UNITY_ANDROID
        float aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
        bool isTablet = (DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f);

        if (isTablet) {
            Debug.Log("Current Devices is AndroidTablet");
            return CurrentDevice.AndroidTablet;
        } else {
            Debug.Log("Current Devices is Android");
            return CurrentDevice.Android;
        }
#endif
        return CurrentDevice.OtherDevice;
    }

    public enum Orientation {
        Portrait,
        Landscape
    }

    public static void SetDeviceOrientation(Orientation orientation,bool autoRotate) {
        if(autoRotate) {
            Screen.orientation = ScreenOrientation.AutoRotation;
            ScreenRotationManager.AutoRotate = true;
            ScreenRotationManager.SetAutoRotation(true);
            return;
        } else {
            switch(orientation) {
                case Orientation.Portrait:
                Screen.orientation = ScreenOrientation.Portrait;
                ScreenRotationManager.SetOrientation(ScreenOrientation.Portrait);
                ScreenRotationManager.AutoRotate = false;
                //DelayChangeRotaion(ScreenOrientation.Portrait);
                break;
                case Orientation.Landscape:
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                ScreenRotationManager.SetOrientation(ScreenOrientation.LandscapeLeft);
                ScreenRotationManager.AutoRotate = false;
                //DelayChangeRotaion(ScreenOrientation.Landscape);
                break;
                default:
                break;
            }
            IEnumerator DelayChangeRotaion(ScreenOrientation orientation) {
                yield return new WaitForSeconds(0.3f);
                Debug.Log("DelayVhangeRotation");
                Screen.orientation = orientation;
                ScreenRotationManager.SetOrientation(orientation);
                ScreenRotationManager.AutoRotate = false;
            }
        }
    }

    public static void EventTriggerContainType(this EventTrigger targetTrigger,EventTriggerType checkingType,out bool isContain,out EventTrigger.Entry outEntry) {
        foreach(var entry in targetTrigger.triggers) {
            if(entry.eventID == checkingType) {
                isContain = true;
                outEntry = entry;
                return;
            }
        }
        isContain = false;
        outEntry = null;
        return;
    }

    public static Color ColorInHex(string hex) {
        if(ColorUtility.TryParseHtmlString(hex,out Color colorPhrased)) {
            return colorPhrased;
        }
        return Color.white;
    }

    public static float RoundtoDp(this float num,int dp = 2) {
        var tempDp = dp * 10;
        return Mathf.Round(num * tempDp) / tempDp;
    }

    public static string RoundtoDpStr(this float num,int dp = 2) {
        var tempDp = Mathf.Pow(10,dp);
        string ans = (Mathf.Round(num * tempDp) / tempDp).ToString();
        if(ans.Split(char.Parse(".")).Length < 2) {
            ans += ".";
            for(int i = 0; i < dp; i++) {
                ans += "0";
            }
            return ans;
        }
        if(ans.Split(".")[1].Length < dp) {
            for(int i = 0; i < (dp - ans.Split(char.Parse("."))[1].Length); i++) {
                ans += "0";
            }
            return ans;
        }
        return ans;
    }

    public static IEnumerator PlayVideo(this VideoPlayer videoPlayer,VideoClip clip,RawImage videoBox,Action<VideoPlayer> callBackAction,bool isloop = false) {
        videoPlayer.Prepare();
        videoPlayer.loopPointReached += (VideoPlayer vp) => { callBackAction(vp); };
        while(!videoPlayer.isPrepared) {
            yield return null;
        }
        videoPlayer.clip = clip;
        videoBox.enabled = true;
        videoBox.texture = videoPlayer.texture;
        videoPlayer.isLooping = isloop;
        videoBox.SizeToParent();
        videoPlayer.Play();
    }
}
