namespace Dictation.Services
{
    using System;
    using Windows.ApplicationModel.Store;

    public static class LicenseService
    {
        public static LicenseInformation LicenseInformation { get; set; }

        public static bool IsFeaturePurchased(string feature)
        {
            if (LicenseInformation.ProductLicenses[feature].IsActive)
            {
                return true;
            }
            else
            {
                BuyFeature(feature);
                return false;
            }
        }

        public static async void BuyFeature(string feature)
        {
            if (!LicenseInformation.ProductLicenses[feature].IsActive)
            {
                try
                {
                    // The customer doesn't own this feature, so
                    // show the purchase dialog.
                    await CurrentAppSimulator.RequestProductPurchaseAsync(feature, false);

                    //Check the license state to determine if the in-app purchase was successful.
                }
                catch (Exception)
                {
                    // The in-app purchase was not completed because
                    // an error occurred.
                }
            }
            else
            {
                // The customer already owns this feature.
            }
        }
    }
}
