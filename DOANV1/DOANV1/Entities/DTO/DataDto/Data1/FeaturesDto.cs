using DOANV1.Extensions.Features.Pagging_Get;

namespace DOANV1.Entities.Extensions_Entities.DTO.DataDto1
{
    
    public class FeaturesDto : RequestFeatures // Lớp ItemParameters sẽ chứa các tham số cụ thể
    {


        // Hỗ trợ Sort (Sắp xếp)
        //public FeaturesDto ()
        //{
        //    OrderBy = "name";
        //}


        // Thuộc tính lọc 
        public uint MinPrice { get; set; }


        // Thuộc tính lọc 
        public uint MaxPrice { get; set; } = int.MaxValue;


        // Thuộc tính lọc 
       public bool ValidPriceRange => MaxPrice > MinPrice; // Chúng tôi cũng đã thêm một thuộc tính xác thực đơn giản – ValidAgePrice . Mục đích là để cho chúng tôi biết liệu giá tối đa có thực sự lớn hơn giá tối thiểu hay không


        // Thuộc tính Search 
        //public string SearchTerm { get; set; }


        // Thuộc tính DataShaping 
        public string Fields { get; set; }

    }
}
