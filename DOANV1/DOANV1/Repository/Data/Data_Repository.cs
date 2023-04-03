using DOANV1.Entities.Extensions_Entities.DTO.DataDto1;
using DOANV1.Entities.Model;
using DOANV1.Entities.Model.Data;
using DOANV1.Extensions.Features.Pagging_Get;
using Interface.IData_Repository;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class Data_Repository : IData_Repository
    {

        private const string databaseName = "Data";   // Thuộc tính tên cơ sở dữ liệu 

        private const string collectionName = "Data_Test"; // Thuộc tính tên của bộ sưu tập 

        private readonly FilterDefinitionBuilder<Data_Model> filterBuilder = Builders<Data_Model>.Filter; // Thuộc tính bộ lọc 

        private readonly IMongoCollection<Data_Model> itemsCollection; // Tạo bộ sưu tập từ lớp Item

        public Data_Repository()
        {
        }

        public Data_Repository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName); // Tham chiếu đến tên cơ sở dữ liệu 

            itemsCollection = database.GetCollection<Data_Model>(collectionName);// Tham chiếu đến tên bộ sưu tập 

        }




        public async Task CreateDataAsync(Data_Model data)
        {
            await itemsCollection.InsertOneAsync(data);
        }

        //public async Task DeleteItemAsync(Guid id)
        //{
        //    var filter = filterBuilder.Eq(item => item.Id, id); // Lọc theo Id
        //    await itemsCollection.DeleteOneAsync(filter); // Mỗi lần thực thi sẽ xóa theo id truyền vào 
        //}



        public async Task<Data_Model> GetDataAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id); // Id phải khớp với Id nhận được từ tham số 
            return await itemsCollection.Find(filter).SingleOrDefaultAsync(); // Phương thức SingleorDefult chỉ cho phép trả về 1 dữ liệu bất kì tìm thấy 
        }
        //Task<PagedList<Data_Model>> GetAllDataAsync(FeaturesDto dataParameter);
        public async Task<PagedList<Data_Model>> GetAllDataAsync(FeaturesDto dataParameter)
        {
           
            // Triển khai điều kiện lọc Fillter
            var item = await itemsCollection.Find(e => e.Price >= dataParameter.MinPrice && e.Price <= dataParameter.MaxPrice).ToListAsync(); // truyền tham số với điều kiện lọc giá trị min max trong cơ sở dữ liệu  
                                                                                                                                              //var item = await itemsCollection.Find(new BsonDocument())
            // Triển khai phân trang
            var paging = PagedList<Data_Model>.ToPagedList(item, dataParameter.PageNumber, dataParameter.PageSize); // Trả về danh sách dữ liệu trích xuất từ csdl ứng với các tham số truyền vào  
            
            return paging;

        }

        public async Task DeleteDataAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id); // Lọc theo Id
            await itemsCollection.DeleteOneAsync(filter); // Mỗi lần thực thi sẽ xóa theo id truyền vào 
        }



        public async Task UpdateDataAsync(Data_Model data)
        {
            var filter = filterBuilder.Eq(exsitingItem => exsitingItem.Id, data.Id); // Lọc theo id 
            await itemsCollection.ReplaceOneAsync(filter, data);
        }


    }
}
