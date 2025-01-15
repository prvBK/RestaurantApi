namespace RestaurantApi.Models
{
    public class PageResult<T>(List<T> items, int totalCount, int pageSize, int pageNumber)
    {
        public int ItemFrom { get; set; } = pageSize * (pageNumber - 1) + 1;
        public List<T> Items { get; set; } = items;
        public int ItemTo { get; set; } = pageSize * (pageNumber - 1) + pageSize;
        public int TotalItemsCount { get; set; } = totalCount;
        public int TotalPages { get; set; } = (int)Math.Ceiling((double)totalCount / pageSize);
    }
}