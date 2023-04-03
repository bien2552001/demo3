using System;

namespace DOANV1.Entities.Extensions_Entities.DTO.DataDto1
{
    
    public record DataDto // Tham khảo thêm về record: https://tuhocict.com/c-9-0-co-gi-moi/
    {
        public Guid Id { get; init; } // init là loại property chỉ có thể gán giá trị 1 lần ...Tham khảo https://tuhocict.com/c-9-0-co-gi-moi/ 

        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreateDate { get; init; }


    }
}
