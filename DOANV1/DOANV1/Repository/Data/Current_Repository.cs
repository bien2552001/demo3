using DOANV1.Entities.Extensions_Entities.DTO.DataDto1;
using DOANV1.Entities.Model.Data;
using DOANV1.Extensions.Features.Pagging_Get;
using DOANV1.Interface.IData;
using MongoDB.Driver;
using System.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using MongoDB.Bson;

namespace DOANV1.Repository.Data
{
    public class Current_Repository: ICurrent_Repository
    {
        private const string databaseName = "Data";   // Thuộc tính tên cơ sở dữ liệu 

        private const string collectionName = "Data_Current"; // Thuộc tính tên của bộ sưu tập 

        private readonly FilterDefinitionBuilder<Current_Model> filterBuilder = Builders<Current_Model>.Filter; // Thuộc tính bộ lọc 

        private readonly IMongoCollection<Current_Model> currentCollection; // Tạo bộ sưu tập từ lớp Item

        public Current_Repository()
        {
        }

        public Current_Repository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName); // Tham chiếu đến tên cơ sở dữ liệu 

            currentCollection = database.GetCollection<Current_Model>(collectionName);// Tham chiếu đến tên bộ sưu tập 

        }




        public async Task CreateCurrentAsync(Current_Model data)
        {
            await currentCollection.InsertOneAsync(data);
        }

  


        public async Task<Current_Model> GetCurrentAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id); // Id phải khớp với Id nhận được từ tham số 
            return await currentCollection.Find(filter).SingleOrDefaultAsync(); // Phương thức SingleorDefult chỉ cho phép trả về 1 dữ liệu bất kì tìm thấy 
        }


        //public async Task<IEnumerable<Current_Model>> GetAllCurrentAsync(/*string Name,*/ DateTimeOffset? from, DateTimeOffset? to)
        //{

        //    var builder = Builders<Current_Model>.Filter;

        //    var filter = builder.Empty;

        //    //if (!string.IsNullOrEmpty(Name))
        //    //{
        //    //    filter &= builder.Eq("Name", Name);
        //    //}
        //    if (from.HasValue)
        //    {
        //        filter &= builder.Gte("dateTime", from.Value);
        //    }
        //    if (to.HasValue)
        //    {
        //        filter &= builder.Lte("dateTime", to.Value);
        //    }

        //    return await Task.FromResult(currentCollection.Find(filter).ToList());

        //}

     
        public async Task<IEnumerable<Current_Model>> GetAllCurrentAsync(DateTimeOffset? from, DateTimeOffset? to)
        {
            var builder = Builders<Current_Model>.Filter;

            var filter = builder.Empty;

            if (from != null)
            {
                filter &= builder.Gte("CreatedDate", from);
            }

            if (to != null)
            {
                filter &= builder.Lte("CreatedDate", to);
            }

            return await Task.FromResult(currentCollection.Find(filter).ToList());
        }




        public async Task DeleteCurrentAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id); // Lọc theo Id
            await currentCollection.DeleteOneAsync(filter); // Mỗi lần thực thi sẽ xóa theo id truyền vào 
        }



        public async Task UpdateCurrentAsync(Current_Model data)
        {
            var filter = filterBuilder.Eq(exsitingItem => exsitingItem.Id, data.Id); // Lọc theo id 
            await currentCollection.ReplaceOneAsync(filter, data);
        }

    }
}
