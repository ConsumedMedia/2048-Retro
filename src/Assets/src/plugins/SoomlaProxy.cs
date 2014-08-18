using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Soomla;

public class SoomlaProxy : MonoBehaviour 
{
    private bool _isOpening = false;
    public UndoButton undoBtn;

    public void restoreTransactions()
    {
        Debug.Log("Soomla  restoreTransactions");
        StoreController.RestoreTransactions();
    }

    void Start() 
    {
        Debug.Log("Soomla  Start");
        StoreController.Initialize(new SoomlaStore());

        Events.OnMarketPurchase += onMarketPurchase;
        Events.OnMarketPurchaseStarted += OnMarketPurchaseStarted;
        Events.OnMarketPurchaseCancelled += OnMarketPurchaseCancelled;
        Events.OnStoreControllerInitialized += onStoreControllerInitialized;
        Events.OnRestoreTransactions += onRestoreTransactions;
    }

    public void buyItem(String itemId)
    {
        Debug.Log("Soomla  buyItem = " + itemId);
        StoreController.BuyMarketItem(itemId);
    }

    public void openStore()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        if (!_isOpening)
        {
            _isOpening = true;
            StoreController.StartIabServiceInBg();
        }
        #endif
    }

    public void closeStore()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        if (_isOpening)
        {
            _isOpening = false;
            StoreController.StopIabServiceInBg();
        }
        #endif
    }

    void onRestoreTransactions(bool result)
    {
        Debug.Log("Soomla  onRestoreTransactions = " + result.ToString());
    }

    void onStoreControllerInitialized()
    {
        Debug.Log("Soomla  onStoreControllerInitialized");
    }


    void OnMarketPurchaseStarted(PurchasableVirtualItem obj)
    {
        Debug.Log("Soomla  OnMarketPurchaseStarted");
        Debug.Log("Going to purchase an item with productId: " + obj.ItemId);
        Debug.Log("Going to purchase an item with productId: " + obj.Name);
        Debug.Log("Going to purchase an item with productId: " + obj.Description);
        Debug.Log("Going to purchase an item with productId: " + obj.PurchaseType);
    }

    void OnMarketPurchaseCancelled(PurchasableVirtualItem obj)
    {
        Debug.Log("Soomla  OnMarketPurchaseCancelled");
        Debug.Log("Going to purchase an item with productId: " + obj.ItemId);
        Debug.Log("Going to purchase an item with productId: " + obj.Name);
        Debug.Log("Going to purchase an item with productId: " + obj.Description);
        Debug.Log("Going to purchase an item with productId: " + obj.PurchaseType);
    }

    void OnUnexpectedErrorInStore()
    {
        Debug.Log("Soomla  OnUnexpectedErrorInStore");
    }

    public void onMarketPurchase(PurchasableVirtualItem pvi) 
    {
        Debug.Log("Soomla  onMarketPurchase");
        Debug.Log("Going to purchase an item with productId: " + pvi.ItemId);
        Debug.Log("Going to purchase an item with productId: " + pvi.Name);
        Debug.Log("Going to purchase an item with productId: " + pvi.Description);
        Debug.Log("Going to purchase an item with productId: " + pvi.PurchaseType);

        if (pvi.ItemId == SoomlaStore.UNDO_PACK_ID_1)
            undoBtn.undoCnt += 5;
        else if (pvi.ItemId == SoomlaStore.UNDO_PACK_ID_2)
            undoBtn.undoCnt += 15;
        else if (pvi.ItemId == SoomlaStore.UNDO_PACK_ID_3)
            undoBtn.undoCnt += 50;
        else if (pvi.ItemId == SoomlaStore.REMOVE_ADS_PRODUCT_ID)
        {
            UserData.adRemoved = true;
            GoogleMobileAdsPlugin.HideBannerView();
        }
    }

}

