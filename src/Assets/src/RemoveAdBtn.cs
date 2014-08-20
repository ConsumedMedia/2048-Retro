using Assets.src.Billing;
using UnityEngine;

public class RemoveAdBtn : MonoBehaviour
{
    public tk2dUIItem btn;

    void Start()
    {
        btn.OnClick += click;
    }

    void click()
    {
        AppBilling.RequestPurchase(MarketLot.RemoveAds);
    }


}
