using System;

namespace DOANV1.Entities.Model.Data
{
    public class Data_Model // Tham khảo thêm về record: https://tuhocict.com/c-9-0-co-gi-moi/
    {
        public Guid Id { get; set; } // init là loại property chỉ có thể gán giá trị 1 lần ...Tham khảo https://tuhocict.com/c-9-0-co-gi-moi/ 

        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreateDate { get; set; }


    }
}
