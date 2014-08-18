using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla;

public class SoomlaStore : IStoreAssets{

    public int GetVersion() {
        return 0;
    }

    public VirtualCurrency[] GetCurrencies() 
    {
        return new VirtualCurrency[]{COINS_CURRENCY};
    }

    public VirtualGood[] GetGoods() 
    {
        return new VirtualGood[] {};
    }

    public VirtualCurrencyPack[] GetCurrencyPacks() 
    {
        return new VirtualCurrencyPack[] {UNDO_PACK_1, UNDO_PACK_2, UNDO_PACK_3};
    }

    public VirtualCategory[] GetCategories() 
    {
        return new VirtualCategory[]{GENERAL_CATEGORY};
    }

    public NonConsumableItem[] GetNonConsumableItems() 
    {
        return new NonConsumableItem[]{REMOVE_ADS};
    }

    /*        * Static Final members **/
    public const string COINS_CURRENCY_ITEM_ID = "coins_currency";
    public const string UNDO_PACK_ID_1 = "com.consumedmedia.retro2048.pack1";
    public const string UNDO_PACK_ID_2 = "com.consumedmedia.retro2048.pack2";
    public const string UNDO_PACK_ID_3 = "com.consumedmedia.retro2048.pack3";
    public const string REMOVE_ADS_PRODUCT_ID   = "com.aconsumedmedia.retro2048removeads";
    /*        * Virtual Currencies **/
    public static VirtualCurrency COINS_CURRENCY = new VirtualCurrency(
        "Coins",
        "",
        COINS_CURRENCY_ITEM_ID
    );

    /*        * Virtual Currency Packs **/

    public static VirtualCurrencyPack UNDO_PACK_1 = new VirtualCurrencyPack(
        "5 undo",                                   // name
        "5 undo",                       // description
        UNDO_PACK_ID_1,                                   // item id
        5,                                         // number of currencies in the pack
        COINS_CURRENCY_ITEM_ID,                        // the currency associated with this pack
        new PurchaseWithMarket(UNDO_PACK_ID_1, 0.99)
    );

    public static VirtualCurrencyPack UNDO_PACK_2 = new VirtualCurrencyPack(
        "15 undo",                                   // name
        "15 undo",                       // description
        UNDO_PACK_ID_2,                                   // item id
        15,                                         // number of currencies in the pack
        COINS_CURRENCY_ITEM_ID,                        // the currency associated with this pack
        new PurchaseWithMarket(UNDO_PACK_ID_2, 1.99)
    );

    public static VirtualCurrencyPack UNDO_PACK_3 = new VirtualCurrencyPack(
        "50 undo",                                   // name
        "50 undo",                       // description
        UNDO_PACK_ID_3,                                   // item id
        50,                                         // number of currencies in the pack
        COINS_CURRENCY_ITEM_ID,                        // the currency associated with this pack
        new PurchaseWithMarket(UNDO_PACK_ID_3, 5.99)
    );

    /*        * Virtual Categories **/
    // The muffin rush theme doesn't support categories, so we just put everything under a general category.
    public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
        "General", new List<string>(new string[] {})
    );


    /*        * Google MANAGED Items **/

    public static NonConsumableItem REMOVE_ADS  = new NonConsumableItem(
        "Remove Ads",
        "Remove all Ads",
        REMOVE_ADS_PRODUCT_ID,
        new PurchaseWithMarket(new MarketItem(REMOVE_ADS_PRODUCT_ID, MarketItem.Consumable.NONCONSUMABLE , 0.99))
    );
}

