namespace DOANV1.Extensions.Features.Pagging_Get
{
    public abstract class RequestFeatures  // chúng ta tạo một lớp trừu tượng để chứa thuộc tính cho tất cả các thực thể trong dự án của chúng tôi

    {
        const int maxPageSize = 50; // Trong lớp trừu tượng, chúng tôi đang sử dụng hằng số maxPageSize để hạn chế API của chúng tôi lên tối đa 50 hàng trên mỗi trang
        public int PageNumber { get; set; } = 1; //  Nếu không được thiết lập bởi người gọi, Số trang sẽ được đặt thành 1 và Kích thước trang thành 10.

        private int _pageSize = 5;//  Nếu không được thiết lập bởi người gọi, Số trang sẽ được đặt thành 1 và Kích thước trang thành 10.

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }


        // Triển khai thuộc tính Sắp xếp (Sort)
        //public string OrderBy { get; set; }




    }
}
