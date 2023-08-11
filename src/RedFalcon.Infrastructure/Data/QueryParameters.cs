namespace RedFalcon.Infrastructure.Data
{
    public class QueryParameters
    {
        private readonly int defaultPageSize = 10;
        private readonly int maximumPageSize = 1000;


        /// <summary>
        /// With Search, Filter, and Pagination functionality
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="searchableFields"></param>
        /// <param name="filters"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        public QueryParameters(string searchQuery, List<string> searchableFields, Dictionary<string, string> filters, int? page, int? pageSize)
        {
            _searchQuery = searchQuery;
            _searchableFields = searchableFields;
            _filters = filters;
            _page = page == null || page == 0 ? 1 : page.Value;
            _pageSize = pageSize == null ? defaultPageSize : pageSize > maximumPageSize ? maximumPageSize : pageSize.Value;
            _hasPaginationParameter = page != null && pageSize != null;
            Parameters = new Dictionary<string, object>();
        }

        /// <summary>
        /// Pagination and Search Only
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        public QueryParameters(string searchQuery, List<string> searchableFields, int? page, int? pageSize)
        {
            _searchQuery = searchQuery;
            _searchableFields = searchableFields;
            _filters = null;
            _page = page == null || page == 0 ? 1 : page.Value;
            _pageSize = pageSize == null ? defaultPageSize : pageSize > maximumPageSize ? maximumPageSize : pageSize.Value;
            _hasPaginationParameter = page != null && pageSize != null;
            Parameters = new Dictionary<string, object>();
        }

        private string _searchQuery { get; set; }
        private List<string> _searchableFields { get; set; }
        private Dictionary<string, string>? _filters { get; set; }

        private int _page { get; set; }
        private int _pageSize { get; set; }
        private bool _hasPaginationParameter { get; set; }

        public Dictionary<string, object> Parameters { get; private set; }

        public bool HasSearchQueryParameter
        {
            get
            {
                return !string.IsNullOrEmpty(_searchQuery);
            }
        }

        public string GetSearchSQLQuery()
        {
            var sqlQuery = "";
            if (HasSearchQueryParameter)
            {
                sqlQuery += " AND (";
                foreach (var field in _searchableFields)
                {
                    sqlQuery += @$" {field} LIKE CONCAT('%', @SearchQuery, '%') OR";
                }

                sqlQuery = sqlQuery.TrimEnd(new char[] { 'O', 'R' });

                sqlQuery += ")";

                Parameters.Add("@SearchQuery", _searchQuery);
            }

            return sqlQuery;

        }


        public bool HasFilterParameter
        {
            get
            {
                return _filters != null && _filters.Any(o => !string.IsNullOrEmpty(o.Value));
            }
        }

        public string GetFilterSQLQuery()
        {
            var sqlQuery = "";
            if (HasFilterParameter)
            {
                sqlQuery += "AND (";
                foreach (var filter in _filters.Where(o => !string.IsNullOrEmpty(o.Value)))
                {
                    sqlQuery += @$" {filter.Key} = @{filter.Key} AND";
                    Parameters.Add(@$"@{filter.Key}", filter.Value);
                }

                sqlQuery = sqlQuery.TrimEnd(new char[] { 'A', 'N', 'D' });

                sqlQuery += ")";
            }

            return sqlQuery;
        }


        public bool HasPaginationParameter
        {
            get
            {
                return _hasPaginationParameter;
            }
        }

        public string GetPaginationSQLQuery()
        {
            var sqlQuery = "";
            if (HasPaginationParameter)
            {
                sqlQuery = $@" ORDER BY Id LIMIT @PageSize OFFSET @Offset";

                Parameters.Add("@Offset", (_page - 1) * _pageSize);
                Parameters.Add("@PageSize", _pageSize);
            }

            return sqlQuery;
        }


        public int GetPage()
        {
            return _page;
        }

        public int GetPageSize()
        {
            return _pageSize;
        }
    }
}
