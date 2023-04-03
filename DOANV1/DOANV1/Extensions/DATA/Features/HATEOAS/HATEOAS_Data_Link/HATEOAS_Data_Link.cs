using DOANV1.Entities.Extensions_Entities.DTO.DataDto1;
using DOANV1.Extensions.Features.HATEOAS.HATEOAS_Models;
using DOANV1.Extensions.Features.ShapeData_Get;
using DOANV1.Interface.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DOANV1.Extensions.Features.HATEOAS.HATEOAS_Data_Link
{

    public class HATEOAS_Data_Link
    {
        private readonly LinkGenerator _linkGenerator;  // Chúng tôi sẽ sử dụng LinkGenerator để tạo liên kết cho phản hồi của chúng tôi

        private readonly IDataShaper<DataDto> _dataShaper; // IDataShaper để định hình dữ liệu của chúng tôi

        public HATEOAS_Data_Link(LinkGenerator linkGenerator, IDataShaper<DataDto> dataShaper)
        {
            _linkGenerator = linkGenerator;

            _dataShaper = dataShaper;
        }



        public LinkResponse TryGenerateLinks(IEnumerable<DataDto> employeesDto, string fields, HttpContext httpContext)
        {
            var shapedItem = ShapeData(employeesDto, fields); // Điều đầu tiên chúng tôi làm là định hình bộ sưu tập của mình

            if (ShouldGenerateLinks(httpContext)) // sau đó, nếu httpContext chứa loại phương tiện được yêu cầu, chúng tôi thêm liên kết đến phản hồi

                return ReturnLinkdedItemssss(employeesDto, fields, httpContext, shapedItem); // . Mặt khác, chúng tôi chỉ trả lại dữ liệu đã định hình của mình.

            return ReturnShapedItems(shapedItem);
        }



        private List<ShapedData_Entities> ShapeData(IEnumerable<DataDto> employeesDto, string fields) => // Phương thức ShapeData thực thi định hình dữ liệu và chỉ trích xuất phần thực thể không có thuộc tính Id.

            _dataShaper.ShapeData(employeesDto, fields).Select(e => e.Entity).ToList();




        private bool ShouldGenerateLinks(HttpContext httpContext) // Trong phương thức ShouldGenerateLinks , chúng tôi trích xuất loại phương tiện từ httpContext
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"]; // Nếu loại phương tiện đó kết thúc bằng hatoas, thì phương thức trả về đúng; ngược lại, nó trả về false 

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }


        private LinkResponse ReturnShapedItems(List<ShapedData_Entities> shapedEmployees) => new LinkResponse { ShapedEntities = shapedEmployees }; // ReturnShapedItems chỉ trả về một LinkResponse mới với thuộc tính ShapedEntities


        private LinkResponse ReturnLinkdedItemssss(IEnumerable<DataDto> itemDto, string fields, HttpContext httpContext, List<ShapedData_Entities> shapedEmployees) //  chúng tôi duyệt qua từng nhân viên và tạo liên kết cho nó bằng cách gọi phương thức CreateLinksForItem 

        {
            var itemDtoList = itemDto.ToList();

            for (var index = 0; index < itemDtoList.Count(); index++)

            {
                var itemLinks = CreateLinksForItem(httpContext, itemDtoList[index].Id, fields);

                shapedEmployees[index].Add("Links", itemLinks);
            }

            var employeeCollection = new LinkCollectionWrapper<ShapedData_Entities>(shapedEmployees);   // chúng tôi chỉ cần thêm nó vào bộ sưu tập shapeEmployees

            var linkedEmployees = CreateLinksForItemCollection(httpContext, employeeCollection); // // Sau đó, chúng tôi bọc bộ sưu tập và tạo các liên kết quan trọng cho toàn bộ bộ sưu tập bằng cách gọi phương thức CreateLinksForItemCollection .

            return new LinkResponse { HasLinks = true, LinkedEntities = linkedEmployees };
        }


        private List<Link> CreateLinksForItem(HttpContext httpContext, Guid id, string fields)
        {
            var links = new List<Link>
            {
                 new Link(_linkGenerator.GetUriByAction(httpContext, "GetItemAsync",  values: new { id, fields }),"self", "GET_ID"), // chúng tôi đang tạo các liên kết bằng cách sử dụng phương thức GetUriByAction của LinkGenerator — chấp nhận HttpContext,                                                                                                                                             // tên của hành động và các giá trị cần có được sử dụng để làm cho URL hợp lệ
                 new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteItemAsync", values: new { id }), "delete_item", "DELETE"),
                 new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateItemAsync", values: new {  id }), "update_item", "PUT"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "PatchItemAsync", values: new {  id }), "partially_update_item", "PATCH")
            };
            return links;
        }



        private LinkCollectionWrapper<ShapedData_Entities> CreateLinksForItemCollection(HttpContext httpContext, LinkCollectionWrapper<ShapedData_Entities> employeesWrapper)
        {
            employeesWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetItemsAsync", values: new { }), "self", "GET"));

            return employeesWrapper;
        }




    }
}
