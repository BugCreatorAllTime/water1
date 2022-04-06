using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizeData
{
    public enum LocalizeLanguage
    {
        L_00_Unknow = 0,
        L_01_English = 1,
        L_02_Vi = 2,
        L_03_Hi = 3,
        L_04_Ja = 4,
        L_05_Fr = 5,
        L_06_Th = 6,
        L_07_Ru = 7,
        L_08_Es = 8,
        L_09_De = 9,
        L_10_Kr = 10,
        L_11_CN = 11,
        //L_12_Pk = 12,
    }

    [System.Serializable]
    public class LocalizeSetting
    {
        public string title = "SETTINGS";
        public string sound = "Sound";
        public string music = "Music";
        public string vibrate = "Vibrate";
        public string restore_purchase = "Restore\nPurchase";
    }

    [System.Serializable]
    public class LocalizeShop
    {
        public string tube = "BOTTLE";
        public string theme = "THEME";
        public string iap = "IAP";
        public string unlock = "Unlock Random";
        public string remove_ads = "REMOVE ADS";
    }

    [System.Serializable]
    public class LocalizeTutorial
    {
        public string tap_to_pour = "Tap to pick the bottle";
        public string guide_text = "";
    }

    public string level = "LEVEL";
    public LocalizeSetting setting;
    public LocalizeShop shop;
    public string tap_to_cont = "Tap to continue";
    public LocalizeTutorial tutorial;


    public enum LocalizeId
    {
        S_0_Level = 0,

        S_1_Setting_0_Title = 100,
        S_1_Setting_1_Sound = 101,
        S_1_Setting_2_Music = 102,
        S_1_Setting_3_Vibrate = 103,
        S_1_Setting_4_RestorePurchase = 104,

        S_2_Shop_0_Tube = 200,
        S_2_Shop_1_Theme = 201,
        S_2_Shop_2_Iap = 202,
        S_2_Shop_3_Unlock = 203,
        S_2_Shop_4_RemoveAds = 204,

        S_3_InGame_TaptoCont = 300,

        S_4_Tutorial_0_Pour = 400,
        S_4_Tutorial_1_Guide = 401
    }

    public string TextOfId(LocalizeId localizeId)
    {
        switch (localizeId)
        {
            case LocalizeId.S_0_Level:
                return level;

            case LocalizeId.S_1_Setting_0_Title:
                return setting.title;

            case LocalizeId.S_1_Setting_1_Sound:
                return setting.sound;

            case LocalizeId.S_1_Setting_2_Music:
                return setting.music;

            case LocalizeId.S_1_Setting_3_Vibrate:
                return setting.vibrate;

            case LocalizeId.S_1_Setting_4_RestorePurchase:
                return setting.restore_purchase;


            case LocalizeId.S_2_Shop_0_Tube:
                return shop.tube;

            case LocalizeId.S_2_Shop_1_Theme:
                return shop.theme;

            case LocalizeId.S_2_Shop_2_Iap:
                return shop.iap;

            case LocalizeId.S_2_Shop_3_Unlock:
                return shop.unlock;

            case LocalizeId.S_2_Shop_4_RemoveAds:
                return shop.remove_ads;

            case LocalizeId.S_3_InGame_TaptoCont:
                return tap_to_cont;

            case LocalizeId.S_4_Tutorial_0_Pour:
                return tutorial.tap_to_pour;

            case LocalizeId.S_4_Tutorial_1_Guide:
                return tutorial.guide_text;
        }

        return "";
    }
}
