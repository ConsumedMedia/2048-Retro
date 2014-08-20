using UnityEngine;

namespace Assets.src.Billing
{
	public abstract class AppBilling : MonoBehaviour
	{
	    private static AppBilling _instance;

        public UndoButton UndoBtn;


		public virtual void Start()
		{
            if(_instance != null)
                Debug.Log("More that one instance initializing.");
		    _instance = this;
		}

		public static void RequestPurchase(MarketLot marketLot)
		{
		    _instance.Purchase(marketLot);
		}

	    public static void RestoreTransactions()
	    {
	        Debug.Log("Restore transactions");
	    }

		protected abstract void Purchase(MarketLot lot);

		protected void CompletePurchase(MarketLot lot)
		{
		    Debug.Log("Purchase completed:" + lot.Id);
            if (lot == MarketLot.UndoPack1)
                UndoBtn.undoCnt += 5;
            else if (lot == MarketLot.UndoPack2)
                UndoBtn.undoCnt += 15;
            else if (lot == MarketLot.UndoPack3)
                UndoBtn.undoCnt += 50;
            else if (lot == MarketLot.RemoveAds)
            {
                UserData.adRemoved = true;
                GoogleMobileAdsPlugin.HideBannerView();
            }
		}
	}
}
