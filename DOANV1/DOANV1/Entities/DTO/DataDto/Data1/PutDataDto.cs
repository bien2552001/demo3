using System.ComponentModel.DataAnnotations;

namespace DOANV1.Entities.Extensions_Entities.DTO.DataDto1
{
    public class PutDataDto
    {
        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; init; }

        [Required(ErrorMessage = "Price is a required field.")]  // trường hợp post dữ liệu (thiếu thuộc tính) thì dữ liệu đó vẫn được tạo bình thường và để ngăn chặn điều đó thì dùng Thuộc tính required này cho biết đây là thuộc tính bắt buộc phải được thêm vào khi thực thi Post dữ liệu 
        [Range(1, 1000)]
        public decimal Price { get; init; }
    }
}
