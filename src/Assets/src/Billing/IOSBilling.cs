using UnityEngine;

namespace Assets.src.Billing
{
	public class IOSBilling : AppBilling
	{
		private bool _canMakePayments;

		public void Awake()
		{
			if (Application.platform != RuntimePlatform.IPhonePlayer)
				gameObject.SetActive(false);
		}

#if !UNITY_IPHONE
		protected override void Purchase(MarketLot lot)
		{
			Debug.Log("Error! Call IOSBilling Purchase from not iOS build.");
		}
#endif

#if UNITY_IPHONE

		public override void Start()
		{
			base.Start();
			Debug.Log("iOS billing is starting.");
			_canMakePayments = StoreKitBinding.canMakePayments();
			Debug.Log("IOBilling. Can make payments:" + _canMakePayments);
            if (_canMakePayments)
            {
                Debug.Log("iOSBilling. Request Product data:" + MarketLot.Coins5000Lot.ProductIOSIdentifier);
                StoreKitBinding.requestProductData(new[] {MarketLot.Coins5000Lot.ProductIOSIdentifier});
            }
		}

		protected override void Purchase(MarketLot lot)
		{
			Debug.Log("IOSBilling. Purchase lot:" + lot);
			if (_canMakePayments)
			{
				StoreKitBinding.purchaseProduct(lot.ProductIOSIdentifier, 1);
			}
		}

		private void PurchaseSuccessful(StoreKitTransaction transaction)
		{
			Debug.Log("IOSBilling. Purchased product: " + transaction);
		    CompletePurchase(MarketLot.Coins5000Lot);
		}

        #region Events

        public void OnEnable()
	    {
            StoreKitManager.purchaseSuccessfulEvent += PurchaseSuccessful;
            StoreKitManager.productListReceivedEvent += OnProductListReceivedEvent;
            StoreKitManager.productPurchaseAwaitingConfirmationEvent += productPurchaseAwaitingConfirmationEvent;
            StoreKitManager.purchaseCancelledEvent += purchaseCancelled;
            StoreKitManager.purchaseFailedEvent += purchaseFailed;
            StoreKitManager.productListRequestFailedEvent += productListRequestFailed;
            StoreKitManager.restoreTransactionsFailedEvent += restoreTransactionsFailed;
            StoreKitManager.restoreTransactionsFinishedEvent += restoreTransactionsFinished;
            StoreKitManager.paymentQueueUpdatedDownloadsEvent += paymentQueueUpdatedDownloadsEvent;	        
	    }

	    public void OnDisable()
	    {
            StoreKitManager.purchaseSuccessfulEvent -= PurchaseSuccessful;
	        StoreKitManager.productListReceivedEvent -= OnProductListReceivedEvent;
            StoreKitManager.productPurchaseAwaitingConfirmationEvent -= productPurchaseAwaitingConfirmationEvent;
            StoreKitManager.purchaseCancelledEvent -= purchaseCancelled;
            StoreKitManager.purchaseFailedEvent -= purchaseFailed;
            StoreKitManager.productListRequestFailedEvent -= productListRequestFailed;
            StoreKitManager.restoreTransactionsFailedEvent -= restoreTransactionsFailed;
            StoreKitManager.restoreTransactionsFinishedEvent -= restoreTransactionsFinished;
            StoreKitManager.paymentQueueUpdatedDownloadsEvent -= paymentQueueUpdatedDownloadsEvent;	        
	    }

        private void OnProductListReceivedEvent(List<StoreKitProduct> productsList)
        {
            Debug.Log("IOSBilling. OnProductListReceivedEvent. Total products received: " + productsList.Count);
            foreach (StoreKitProduct product in productsList)
                Debug.Log(product);
        }

		private void purchaseFailed(string error)
		{
			Debug.Log("IOSBilling. Purchase failed with error: " + error);
		}

		private void purchaseCancelled(string error)
		{
			Debug.Log("IOSBilling. Purchase cancelled with error: " + error);
		}

		private void productListRequestFailed(string error)
		{
			Debug.Log("IOSBilling. ProductListRequestFailed: " + error);
		}

		private void productPurchaseAwaitingConfirmationEvent(StoreKitTransaction transaction)
		{
			Debug.Log("IOSBilling. ProductPurchaseAwaitingConfirmationEvent: " + transaction);
		}

		private void restoreTransactionsFailed(string error)
		{
			Debug.Log("IOSBilling. RestoreTransactionsFailed: " + error);
		}

		private void restoreTransactionsFinished()
		{
			Debug.Log("IOSBilling. RestoreTransactionsFinished");
		}

		private void paymentQueueUpdatedDownloadsEvent(List<StoreKitDownload> downloads)
		{
			Debug.Log("IOSBilling. PaymentQueueUpdatedDownloadsEvent: ");
			foreach (var dl in downloads)
				Debug.Log(dl);
        }

        #endregion
#endif
    }
}
