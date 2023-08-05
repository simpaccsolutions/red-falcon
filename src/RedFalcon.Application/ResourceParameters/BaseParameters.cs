namespace RedFalcon.Application.ResourceParameters
{
    public class BaseParameters
    {
        private const int _defaultPageSize = 10;
        private const int _maximumPageSize = 1000;

        public List<string>? SearchFields { get; set; }
        public string? Search { get; set; }

        public string? OrderBy { get; set; }

        // Pagination
        public int Page { get; set; } = 1;

        private int _pageSize = _defaultPageSize;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > _maximumPageSize) ? _maximumPageSize : value;
            }
        }
    }
}
