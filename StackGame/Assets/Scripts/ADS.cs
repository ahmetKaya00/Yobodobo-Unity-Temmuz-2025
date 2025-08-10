using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADS : MonoBehaviour
{
    private string _adUnitBnnr = "ca-app-pub-3940256099942544/6300978111";
    private string _adUnitGecis = "ca-app-pub-3940256099942544/1033173712";
    private string _adUnitOdl = "ca-app-pub-3940256099942544/5224354917";

    BannerView _bannerView;
    InterstitialAd _interstitialAd;
    RewardedAd _rewardedAd;
    void Start()
    {
        MobileAds.Initialize(initstatus => { LoadAd(); LoadInterstialAd(); LoadRewardAd(); });
    }

    public void LoadAd()
    {
        if(_bannerView == null)
        {
            CreateBannerView();
        }
        var adRequest = new AdRequest();
        _bannerView.LoadAd(adRequest);
    }
    public void CreateBannerView()
    {
        if (_bannerView != null)
        {
            DestroyAd();
        }
        _bannerView = new BannerView(_adUnitBnnr,AdSize.Banner,AdPosition.Bottom);
    }
    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            _bannerView.Destroy();  
            _bannerView = null;
        }
    }

    public void LoadInterstialAd()
    {
        if(_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }
        var adRequest = new AdRequest();

        InterstitialAd.Load(_adUnitGecis, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if(error != null || ad == null)
            {   

                return;
            }
            _interstitialAd = ad;
        });
    }
    public void ShowIntersitialAd()
    {
        if(_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            _interstitialAd.Show();
        }
        else
        {
            Debug.Log("HATA!");
        }
    }
    public void LoadRewardAd()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }
        var adRequest = new AdRequest();

        RewardedAd.Load(_adUnitOdl, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {

                return;
            }
            _rewardedAd = ad;
        });
    }
    public void ShowRewardedAd()
    {
        const string rewerdMsg = "Odullu reklam gonderildi. Tür: {0},miktart {1}";

        if(_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                Debug.Log(string.Format(rewerdMsg, reward.Type, reward.Amount));
            });
        }
        else
        {
            Debug.Log("Odullu reklam hatasý");
        }
    }


}
