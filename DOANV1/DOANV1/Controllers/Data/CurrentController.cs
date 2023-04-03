

using AutoMapper;
using DOANV1.Entities.DTO.DataDto.CurrentDto;
using DOANV1.Entities.Extensions_Entities.DTO.DataDto1;
using DOANV1.Entities.Model.Data;
using DOANV1.Extensions.DATA.Current_Chart.Chart_Hub;
using DOANV1.Extensions.Features.HATEOAS.HATEOAS_Data_Link;
using DOANV1.Extensions.Service.ActionFilters;
using DOANV1.Interface.IData;
using Interface.IService;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DOANV1.Controllers.Data
{

    [Route("data/current")]
    //[Authorize]
    [ApiController]
    [ResponseCache(CacheProfileName = "120SecondsDuration")] // Cache toàn bộ điều khiển ( ngoại trừ cache response trong Get) 
    public class CurrentController : ControllerBase
    {

        private readonly ICurrent_Repository _repository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;


        public CurrentController(ICurrent_Repository repository, ILoggerService logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
    
        [HttpGet]
        public async Task<IEnumerable<Current_Model>> GetAllCurrentAsync([FromQuery] DateTimeOffset? from, [FromQuery] DateTimeOffset? to)
        {
            var users = await _repository.GetAllCurrentAsync(from, to);
            //await _hub.Clients.All.SendAsync("TransferChartData", users);
            return users;
        }


        [HttpGet("{id}")]
        [ServiceFilter(typeof(AsyncActionFilter), Order = 2)] // Triển khai bộ lọc , với thuộc tính Order là thứ tự triển khai bộ lọc có thể có hoặc không 
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetCurrentAsync(Guid id)
        {

            var checkId = await _repository.GetCurrentAsync(id);

            if (checkId == null) return NotFound($" ======>>>>>>>  GET_ID with id: {id} doesn't exist in the database.");

            var itemDto = _mapper.Map<Current_ReturnDto>(checkId);

            _logger.LogInfo("========>>>>>>> GET_Id  successful");

            return Ok(itemDto);

        }




        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter), Order = 3)]// Triển khai bộ lọc , với thuộc tính Order là thứ tự triển khai bộ lọc có thể có hoặc không 
        public async Task<IActionResult> CreateItemAsync([FromBody] Post_CurrentDto item)
        {

            var itemCreateDto = _mapper.Map<Current_Model>(item); // chúng tôi ánh xạ Item để tạo thành thực thể itemCreateDto

            await _repository.CreateCurrentAsync(itemCreateDto); // gọi phương thức kho lưu trữ để tạo itemCreateDto mới trong cơ sở dữ liệu 

            var itemToReturn = _mapper.Map<Current_ReturnDto>(itemCreateDto); //  chúng tôi ánh xạ thực thể Item cho đối tượng ItemDto để trả lại cho khách hàng.

            _logger.LogInfo("========>>>>>>> POST successful");

            return CreatedAtAction(nameof(GetAllCurrentAsync), new { id = itemToReturn.Id }, itemToReturn);// THam khảo phương thức CreateAtAction tại: https://ochzhen.com/blog/created-createdataction-createdatroute-methods-explained-aspnet-core
                                                                                                           // Khi chạy bất đồng bộ thì phương thức CreatedAtAction(nameof(GetItemAsync) sẽ xóa đi hậu tố Async và để khắc phục điều đó thì trong lớp startup cấu hình lại phương thức addController
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilter), Order = 6)]// Triển khai bộ lọc , với thuộc tính Order là thứ tự triển khai bộ lọc có thể có hoặc không 
        public async Task<IActionResult> UpdateCurrentAsync(Guid id, [FromBody] Post_CurrentDto itemDto) // Thuộc tính FromBody cho phép chình sửa nội dung theo id truyền vào trong cơ sở dữ liệu 
        {

            var checkId = await _repository.GetCurrentAsync(id); // Kiểm tra id có tồn tại trong cơ sở dữ liệu hay không

            if (checkId == null) return NotFound($" ======>>>>>>>  PUT_ID with id: {id} doesn't exist in the database.");

            var a = _mapper.Map(itemDto, checkId);

            await _repository.UpdateCurrentAsync(a);

            _logger.LogInfo("========>>>>>>> PUT_Id  successful");

            return NoContent();
        }


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCurrentAsync(Guid id)
        //{
        //    var checkId = await _repository.GetCurrentAsync(id); // Kiểm tra id có tồn tại trong cơ sở dữ liệu hay không

        //    if (checkId == null) return NotFound($" ======>>>>>>>  DELETE_ID with id: {id} doesn't exist in the database.");   // Nếu không tồn tại thì trả về null 

        //    await _repository.DeleteCurrentAsync(id);

        //    _logger.LogInfo("========>>>>>>> DELETE_Id  successful");

        //    return NoContent();

        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrentAsync(Guid id, [FromQuery] DateTimeOffset? from, [FromQuery] DateTimeOffset? to)
        {
            var checkId = await _repository.GetCurrentAsync(id);

            if (checkId == null) return NotFound($"DELETE_ID with id: {id} doesn't exist in the database.");

            await _repository.DeleteCurrentAsync(id);

            _logger.LogInfo("DELETE_Id successful");

            // Lấy lại danh sách dữ liệu sau khi xóa
            var data = await _repository.GetAllCurrentAsync(from, to);

            return Ok(data);
        }

    }
}