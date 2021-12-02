namespace CareerCase.Domain
{
    public static class Constants
    {
        public const int JobLimit = 2;
        public const int JobDayLimit = 15;
        public const int WordsCacheDayCount = 180;

        #region CacheKeys

        public const string UnfavorableWordsCacheKey = nameof(UnfavorableWordsCacheKey);

        #endregion
    }
}