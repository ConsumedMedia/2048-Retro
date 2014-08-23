using System.Collections.Generic;

namespace Assets.src.Billing
{
	public class MarketLot
	{
        public static MarketLot UndoPack1 { get; private set; }
        public static MarketLot UndoPack2 { get; private set; }
        public static MarketLot UndoPack3 { get; private set; }
        public static MarketLot RemoveAds { get; private set; }

        public string Id { get; set; }
		public int Amount { get; set; }
		public float Price { get; set; }
		public string ProductIOSIdentifier { get; set; }
		public string ProductAndroidIdentifier { get; set; }

        public static List<MarketLot> AllLots { get; set; } 

		static MarketLot()
		{
		    RemoveAds = new MarketLot
		    {
		        Id = "RemoveAds",
		        Price = 0.99f,
                ProductIOSIdentifier = "com.consumedmedia.retro2048removeads",
                ProductAndroidIdentifier = "com.consumedmedia.retro2048removeads"
		    };

		    UndoPack1 = new MarketLot
		    {
                Id = "UndoPack1",
		        Amount = 5,
		        Price = 0.99f,
                ProductIOSIdentifier = "com.consumedmedia.retro2048.pack1",
                ProductAndroidIdentifier = "com.consumedmedia.retro2048.pack1"
		    };

            UndoPack2 = new MarketLot
            {
                Id = "UndoPack2",
                Amount = 15,
                Price = 1.99f,
                ProductIOSIdentifier = "com.consumedmedia.retro2048.pack2",
                ProductAndroidIdentifier = "com.consumedmedia.retro2048.pack2"
            };

            UndoPack3 = new MarketLot
            {
                Id = "UndoPack3",
                Amount = 50,
                Price = 5.99f,
                ProductIOSIdentifier = "com.consumedmedia.retro2048.pack3",
                ProductAndroidIdentifier = "com.consumedmedia.retro2048.pack3"
            };

		    AllLots = new List<MarketLot> {RemoveAds, UndoPack1, UndoPack2, UndoPack3};
		}

		public override string ToString()
		{
			return string.Format("Market lot:" + ProductIOSIdentifier);
		}
	}
}
