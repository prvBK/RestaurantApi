namespace RestaurantApi.Models
{
    public class PageResult<T>(List<T> items, int totalCount, int pageSize, int pageNumber)
    {
        public List<T> Items { get; set; } = items;
        public int TotalPages { get; set; } = (int)Math.Ceiling((double)totalCount / pageSize);
        public int ItemFrom { get; set; } = pageSize * (pageNumber - 1) + 1;
        public int ItemTo { get; set; } = pageSize * (pageNumber - 1) + pageSize;
        public int TotalItemsCount { get; set; } = totalCount;
    }
}
