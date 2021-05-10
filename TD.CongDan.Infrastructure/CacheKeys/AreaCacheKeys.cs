namespace TD.CongDan.Infrastructure.CacheKeys
{
    public static class AreaCacheKeys
    {
        public static string ListKey => "AreaList";

        public static string SelectListKey => "AreaSelectList";

        public static string GetKey(int Id) => $"Area-{Id}";

        public static string GetDetailsKey(int Id) => $"AreaDetails-{Id}";
    }
}