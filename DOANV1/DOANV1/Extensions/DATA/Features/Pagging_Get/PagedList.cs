using System.Collections.Generic;
using System;
using DOANV1.Extensions.Features.Pagging;
using System.Linq;

namespace DOANV1.Extensions.Features.Pagging_Get
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount_Data_from_Database = count,

                PageSize = pageSize,

                CurrentPage = pageNumber,

                TotalPages = (int)Math.Ceiling(count / (double)pageSize) // TotalPages được tính bằng cách chia số mục cho trang kích thước và sau đó làm tròn nó thành số lớn hơn vì trang cần phải tồn tại ngay cả khi chỉ có một mục trên đó.

            };

            AddRange(items);
        }


        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();

            var items = source

            .Skip((pageNumber - 1) * pageSize) // Như bạn có thể thấy, chúng ta đã chuyển logic Skip/Take sang phương thức tĩnh bên trong lớp PagedList 

            .Take(pageSize)

            .ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
