
using AutoMapper;
using DOANV1.Entities.Extensions_Entities.DTO.DataDto1;
using DOANV1.Entities.Model.Data;
using DOANV1.Extensions.Features.HATEOAS.HATEOAS_Data_Link;
using DOANV1.Extensions.Service.ActionFilters;
using Interface.IData_Repository;
using Interface.IService;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DOANV1.Controllers
{
    [Route("data")]
    //[Authorize]
    [ApiController]
    [ResponseCache(CacheProfileName = "120SecondsDuration")] // Cache toàn bộ điều khiển ( ngoại trừ cache response trong Get) 
    public class DataController : ControllerBase
    {

        private readonly IData_Repository _repository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly HATEOAS_Data_Link _datalink;


        public DataController(IData_Repository repository, ILoggerService logger, IMapper mapper, HATEOAS_Data_Link datalink)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _datalink = datalink;
        }




        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute_HATEOAS), Order = 1)]  // Triển khai bộ lọc , với thuộc tính Order là thứ tự triển khai bộ lọc có thể có hoặc không 
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetAllDataAsync([FromQuery] FeaturesDto feature) // [FromQuery] thuộc tính này cho phép truy vấn tiêu đề với các tham số truyền vào , ItemParameters lớp này kế thừa từ lớp abstract và hỗ trư cho việc phân trang 
        {

            // Feature___Fillter__kiểm tra điều kiện lọc
            if (!feature.ValidPriceRange) return BadRequest("==========>>>>>>>>   Max Price can't be less than min Price.");

            var item = await _repository.GetAllDataAsync(feature);

            //Pagging____tiêu đề phản hồi 
            Response.Headers.Add("Feature___Pagination", JsonConvert.SerializeObject(item.MetaData));// Phản hồi tiêu đề trong Paging

            var itemDto = _mapper.Map<IEnumerable<DataDto>>(item); // Ánh xạ dữ liệu từ lớp Item đến lớp ItemDto và trả về danh sách  

            _logger.LogInfo("========>>>>>>> GET successful");

            //HATEOAS
            var links = _datalink.TryGenerateLinks(itemDto, feature.Fields, HttpContext);

            return links.HasLinks ? Ok(links.LinkedEntities) : Ok(links.ShapedEntities);

  
        }




        [HttpGet("{id}")]
        [ServiceFilter(typeof(AsyncActionFilter), Order = 2)] // Triển khai bộ lọc , với thuộc tính Order là thứ tự triển khai bộ lọc có thể có hoặc không 
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetDataAsync(Guid id)
         {

            var checkId = await _repository.GetDataAsync(id);

            if (checkId == null) return NotFound($" ======>>>>>>>  GET_ID with id: {id} doesn't exist in the database.");

            var itemDto = _mapper.Map<DataDto>(checkId);

            _logger.LogInfo("========>>>>>>> GET_Id  successful");

             return Ok(itemDto);
 
         }


        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter), Order = 3)]// Triển khai bộ lọc , với thuộc tính Order là thứ tự triển khai bộ lọc có thể có hoặc không 
        public async Task<IActionResult> CreateItemAsync([FromBody] PostDataDto item)
        {

            var itemCreateDto = _mapper.Map<Data_Model>(item); // chúng tôi ánh xạ Item để tạo thành thực thể itemCreateDto

            await _repository.CreateDataAsync(itemCreateDto); // gọi phương thức kho lưu trữ để tạo itemCreateDto mới trong cơ sở dữ liệu 

            var itemToReturn = _mapper.Map<DataDto>(itemCreateDto); //  chúng tôi ánh xạ thực thể Item cho đối tượng ItemDto để trả lại cho khách hàng.

            _logger.LogInfo("========>>>>>>> POST successful");

            return CreatedAtAction(nameof(GetAllDataAsync), new { id = itemToReturn.Id }, itemToReturn);// THam khảo phương thức CreateAtAction tại: https://ochzhen.com/blog/created-createdataction-createdatroute-methods-explained-aspnet-core
                                                                                                            // Khi chạy bất đồng bộ thì phương thức CreatedAtAction(nameof(GetItemAsync) sẽ xóa đi hậu tố Async và để khắc phục điều đó thì trong lớp startup cấu hình lại phương thức addController
        }

    


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemAsync(Guid id)
        {
            var checkId = await _repository.GetDataAsync(id); // Kiểm tra id có tồn tại trong cơ sở dữ liệu hay không

            if (checkId == null)  return NotFound($" ======>>>>>>>  DELETE_ID with id: {id} doesn't exist in the database.");   // Nếu không tồn tại thì trả về null 
          
            await _repository.DeleteDataAsync(id);

            _logger.LogInfo("========>>>>>>> DELETE_Id  successful");

            return NoContent();

        }



        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilter), Order = 6)]// Triển khai bộ lọc , với thuộc tính Order là thứ tự triển khai bộ lọc có thể có hoặc không 
        public async Task<IActionResult> UpdateItemAsync(Guid id, [FromBody] PutDataDto itemDto) // Thuộc tính FromBody cho phép chình sửa nội dung theo id truyền vào trong cơ sở dữ liệu 
        {

            var checkId = await _repository.GetDataAsync(id); // Kiểm tra id có tồn tại trong cơ sở dữ liệu hay không
            
            if (checkId == null) return NotFound($" ======>>>>>>>  PUT_ID with id: {id} doesn't exist in the database.");

            var a = _mapper.Map(itemDto, checkId);

            await _repository.UpdateDataAsync(a); 

            _logger.LogInfo("========>>>>>>> PUT_Id  successful");

            return NoContent();
        }




        [HttpPatch("{id}")]
        //[ServiceFilter(typeof(ValidateIdItemExists), Order = 5)]// Triển khai bộ lọc , với thuộc tính Order là thứ tự triển khai bộ lọc có thể có hoặc không
        public async Task<IActionResult> PatchItemAsync(Guid id, [FromBody] JsonPatchDocument<PatchDataDto> checkConfig)  // Chúng tôi đang chấp nhận JsonPatchDocument từ nội dung yêu cầu
        {
          
            if (checkConfig == null) return BadRequest("========>>>>>>>  PATCH_Config___ object is null"); // Kiểm tra patchDoc = null nếu item không tồn tại trong cơ sở dữ liệu 
          
            var checkId = await _repository.GetDataAsync(id); // Kiểm tra id có tồn tại trong cơ sở dữ liệu hay không

            if (checkId == null) return NotFound($" ======>>>>>>>  PATCH_ID with id: {id} doesn't exist in the database.");// Nếu không tồn tại thì trả về null 

            var itemToPatch = _mapper.Map<PatchDataDto>(checkId); // ánh xạ thuộc tính từ Item đến PatchItemDto

            checkConfig.ApplyTo(itemToPatch,ModelState); // áp dụng itemToPatch

            TryValidateModel(itemToPatch); // Kiểm tra xem mo hình của đối tượng itemToPatch có hợp lệ ko 

            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            var map = _mapper.Map(itemToPatch, checkId);

            await _repository.UpdateDataAsync(map); // Phần Patch này sử dụng lại phần UpdateItemAsync cửa Api PUT
            
            _logger.LogInfo("========>>>>>>> PATCH_Id  successful");
            
            return NoContent();

        }




        [HttpOptions]
        public IActionResult GetItemsOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, PUT, DELETE");

            return Ok();
        }









    }
}
