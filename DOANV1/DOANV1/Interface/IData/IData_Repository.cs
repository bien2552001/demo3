
using DOANV1.Entities.Extensions_Entities.DTO.DataDto1;
using DOANV1.Entities.Model.Data;
using DOANV1.Extensions.Features.Pagging_Get;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface.IData_Repository
{
    public interface IData_Repository
    {
        Task<Data_Model> GetDataAsync(Guid id); // Lấy  dữ liệu theo id 

        Task<PagedList<Data_Model>> GetAllDataAsync(FeaturesDto dataParameter);
        Task CreateDataAsync(Data_Model data); // Tạo ra dữ liệu trong kho dữ liệu 

        Task UpdateDataAsync(Data_Model data);

        Task DeleteDataAsync(Guid id);
    }
}
