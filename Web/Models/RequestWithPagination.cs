namespace DotNetApi.Web.Models
{
    public abstract class RequestWithPagination
    {
        // Default minimum values for all classes implementing pagination
        private const int DefaultMinPageNumber = 1;
        private const int DefaultMinPageSize = 1;

        // Calculated allowed min values - cannot go lower than default min
        private readonly int _minPageNumber;
        private readonly int _minPageSize;

        private int _pageNumber;
        public int PageNumber
        {
            get => _pageNumber;
            set { _pageNumber = GetMinValue(value, _minPageNumber); }
        }

        private int _pageSize;
        public int PageSize
        {
            get => _pageSize;
            set { _pageSize = GetMinValue(value, _minPageSize); }
        }

        protected RequestWithPagination(
            int minPageNumber,
            int minPageSize)
        {
            _minPageNumber = GetMinValue(minPageNumber, DefaultMinPageNumber);
            _pageNumber = _minPageNumber;

            _minPageSize = GetMinValue(minPageSize, DefaultMinPageSize);
            _pageSize = _minPageSize;
        }

        /// <summary>
        /// If provided value (current) is lower than minimum allowed value (fallback) then minmum allowed value will be returned.
        /// In other case the current value will be returned and used.
        /// </summary>
        /// <param name="currentValue">The value (current) to check.</param>
        /// <param name="minAllowedValue">The min allowed value (fallback).</param>
        /// <returns></returns>
        private static int GetMinValue(int currentValue, int minAllowedValue)
        {
            return currentValue < minAllowedValue ? minAllowedValue : currentValue;
        }
    }
}
