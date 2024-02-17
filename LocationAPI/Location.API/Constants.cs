namespace Location.API;

public struct Constants
{
    public struct ApiEndPoints { 
        public struct Pharmacy
        {
            public const string BaseRoute = "api/v1/[controller]";
            public const string GetAvailablePharmacies = "getAvailablePharmacies";
            public const string AddPharmacy = "addPharmacy";
            public const string GetAvailablePharmacyIntervals = "getAvailablePharmacyIntervals";
        }
    }
}
