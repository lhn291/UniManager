namespace UniManager.Application.Result
{
    public class PagedResult<T>
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<T>? Items { get; set; }

        public PagedResult(List<T>? items, int pageNumber, int pageSize, int totalItems)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("pageNumber and pageSize must be greater than 0.");
            }

            TotalItems = totalItems;
            TotalPages = CalculateTotalPages(totalItems, pageSize);

            if (pageNumber > TotalPages)
            {
                throw new ArgumentException("pageNumber exceeds the total number of pages.");
            }

            PageNumber = pageNumber;
            PageSize = pageSize;
            Items = items;
        }

        private static int CalculateTotalPages(int totalItems, int pageSize)
        {
            return (int)Math.Ceiling((double)totalItems / pageSize);
        }
    }
}
