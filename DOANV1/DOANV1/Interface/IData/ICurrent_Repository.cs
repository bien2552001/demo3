using DOANV1.Entities.Extensions_Entities.DTO.DataDto1;
using DOANV1.Entities.Model.Data;
using DOANV1.Extensions.Features.Pagging_Get;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using DOANV1.Entities.DTO.DataDto.CurrentDto;

namespace DOANV1.Interface.IData
{
    public interface ICurrent_Repository
    {

        //Task<IEnumerable<Current_Model>> GetAllCurrentAsync(/*string Name,*/ DateTimeOffset? from, DateTimeOffset? to);
        Task<IEnumerable<Current_Model>> GetAllCurrentAsync(DateTimeOffset? from, DateTimeOffset? to);

        Task<Current_Model> GetCurrentAsync(Guid id); // Lấy  dữ liệu theo id 
        Task CreateCurrentAsync(Current_Model data); // Tạo ra dữ liệu trong kho dữ liệu 

        Task UpdateCurrentAsync(Current_Model data);

        Task DeleteCurrentAsync(Guid id);


    }
}
