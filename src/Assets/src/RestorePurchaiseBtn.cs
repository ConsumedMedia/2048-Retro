using Assets.src.Billing;
using UnityEngine;

public class RestorePurchaiseBtn : MonoBehaviour
{
    public tk2dUIItem btn;

    void Start()
    {
        btn.OnClick += click;
    }

    void click()
    {
        AppBilling.RestoreTransactions();
    }
}
