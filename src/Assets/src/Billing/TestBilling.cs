using UnityEngine;

namespace Assets.src.Billing
{
	public class TestBilling : AppBilling
	{
		public void Awake()
		{
            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.Android)
                gameObject.SetActive(false);
		}

		protected override void Purchase(MarketLot lot)
		{
			CompletePurchase(lot);
		}
	}
}
