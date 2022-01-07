using System.Text;

namespace Services.WebFramework.Pagination
{
    public class PaginationParameters
    {
        private const int MaxPageSize = 750;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 50;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
