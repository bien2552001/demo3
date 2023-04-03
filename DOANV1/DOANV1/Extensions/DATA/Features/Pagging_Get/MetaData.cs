namespace DOANV1.Extensions.Features.Pagging
{
    public class MetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount_Data_from_Database { get; set; }
        public bool HasPrevious => CurrentPage > 1; // HasPrevious là true nếu CurrentPage lớn hơn 1
        public bool HasNext => CurrentPage < TotalPages; //  HasNext là được tính nếu CurrentPage nhỏ hơn tổng số trang TotalPages.



    }
}
