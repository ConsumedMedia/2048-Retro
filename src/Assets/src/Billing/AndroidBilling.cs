using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.src.Billing
{
	public class AndroidBilling : AppBilling
	{
        private const string KEY = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAp+WS9HeJRY5Z7yHF0z9DSXicJjaRNj1TgG1uTlIAwwt5LluufJSN3cfF3iSEgUwG/k6PSs7j8xIZLmuV0wAsMWko6YmoJV6vxArg4BqKxtV6lGWOM3jm6NK3mNbugb3ZRAqoHz95LOY4Fv9RitvCsJS+Sbx4qZKGgPxvBQwThmQmXss9SpEBcdrTn4BZ6eQxQiHeDI1Ra0tNb7B/hSkaBADrZQscjeGcvKMJGCE8kpPsFedpKe9XuqRgHhOwJrAbUdml7yl8N6ZcSx+EhHH2bffxT/gJvG2ExWxOEV5wzH8vXnwChYyJ78ipBh+GRm1dvtn7wSZmg94pLj5OKlYohwIDAQAB";

		public void Awake()
		{
			if (Application.platform != RuntimePlatform.Android)
				gameObject.SetActive(false);
		}

#if !UNITY_ANDROID
		protected override void Purchase(MarketLot lot)
		{
			Debug.Log("Error! Call AndroidBilling Purchase from not Android build.");
		}
#endif

#if UNITY_ANDROID

		public override void Start()
		{
			base.Start();
			Debug.Log("Android billing is starting.");
			SubscribeToEvents();
			GoogleIAB.enableLogging(true);
			GoogleIAB.init(KEY);
		    GoogleIAB.queryInventory(MarketLot.AllLots.Select(l => l.ProductAndroidIdentifier).ToArray());
		}

		private void QueryInventorySucceededEvent(List<GooglePurchase> purchases, List<GoogleSkuInfo> skus)
		{
			Debug.Log("queryInventorySucceededEvent");
			Prime31.Utils.logObject(purchases);
			foreach (GooglePurchase purchase in purchases)
				if (purchase.purchaseState == GooglePurchase.GooglePurchaseState.Purchased)
					ConsumeProduct(purchase);
			Prime31.Utils.logObject(skus);
		}

		private void ConsumeProduct(GooglePurchase purchase)
		{
			Debug.Log("Try consume product:" + purchase.productId);
			GoogleIAB.consumeProduct(purchase.productId);
		}

		protected override void Purchase(MarketLot lot)
		{
			Debug.Log("AndroidBilling. Purchase lot:" + lot);
			GoogleIAB.purchaseProduct(lot.ProductAndroidIdentifier);
		}

		private void purchaseSucceededEvent(GooglePurchase purchase)
		{
			Debug.Log("purchaseSucceededEvent: " + purchase);
			ConsumeProduct(purchase);
		}

		private void consumePurchaseSucceededEvent(GooglePurchase purchase)
		{
			Debug.Log("consumePurchaseSucceededEvent: " + purchase);
		    CompletePurchase(MarketLot.AllLots.Single(l => l.ProductAndroidIdentifier == purchase.productId));
		}

		private void SubscribeToEvents()
		{
			GoogleIABManager.queryInventorySucceededEvent += QueryInventorySucceededEvent;
			GoogleIABManager.queryInventoryFailedEvent += QueryInventoryFailedEvent;
			GoogleIABManager.billingSupportedEvent += BillingSupportedEvent;
			GoogleIABManager.billingNotSupportedEvent += BillingNotSupportedEvent;
			GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
			GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
			GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
			GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
			GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
		}

		private void QueryInventoryFailedEvent(string error)
		{
			Debug.Log("queryInventoryFailedEvent: " + error);
		}

		private void BillingSupportedEvent()
		{
			Debug.Log("billingSupportedEvent");
		}

		void BillingNotSupportedEvent(string error)
		{
			Debug.Log("billingNotSupportedEvent: " + error);
		}

		void purchaseCompleteAwaitingVerificationEvent(string purchaseData, string signature)
		{
			Debug.Log("purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature);
		}

		void purchaseFailedEvent(string error)
		{
			Debug.Log("purchaseFailedEvent: " + error);
		}

		void consumePurchaseFailedEvent(string error)
		{
			Debug.Log("consumePurchaseFailedEvent: " + error);
		}

#endif

	}
}
