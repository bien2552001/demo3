using AutoMapper;
using DOANV1.Entities.DTO.DataDto.CurrentDto;
using DOANV1.Entities.DTO.UserDto.RegisterDto;
using DOANV1.Entities.DTO.UserDto.RolesDto;
using DOANV1.Entities.Extensions_Entities.DTO.DataDto1;
using DOANV1.Entities.Model.Data;
using DOANV1.Entities.Model.User;

namespace DOANV1.Extensions.Service.Mapping
{
  
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // _________________________________DATA__________________________________


            // Ánh xạ cho phương thức Get
            CreateMap<Data_Model, DataDto>();//Ánh xạ thuộc tính từ lớp Item đến lớp ItemDto

            CreateMap<DataDto, Data_Model> ();//Ánh xạ thuộc tính từ lớp Item đến lớp ItemDto


            //CreateMap<Data_Model, Test2Dto>();//Ánh xạ thuộc tính từ lớp Item đến lớp ItemDto


            // Ánh xạ cho phương thức Post
            CreateMap<PostDataDto, Data_Model>();


            // Ánh xạ cho phương thức Put
            CreateMap<PutDataDto, Data_Model>(); // Phương thức ReverseMap() hỗ trợ đảo ngược ánh xạ khi cần 
            //CreateMap<UpdateItemDto, Item>().ReverseMap(); // Phương thức ReverseMap() hỗ trợ đảo ngược ánh xạ khi cần 

            // Ánh xạ cho phương thức Patch
            CreateMap<PatchDataDto, Data_Model>().ReverseMap(); // Phương thức ReverseMap() hỗ trợ đảo ngược ánh xạ khi cần 

            //CreateMap<RegisterRequestDto, User_Model>();


            //_________________________________________Current________________________________________________

            //CreateMap< Get_CurrentDto>();//Ánh xạ thuộc tính từ lớp Item đến lớp ItemDto
            
            CreateMap<Post_CurrentDto, Current_Model>();//Ánh xạ thuộc tính từ lớp Item đến lớp ItemDto

            CreateMap<Current_Model, Current_ReturnDto>();//Ánh xạ thuộc tính từ lớp Item đến lớp ItemDto









            //________________________________________USER_____________________________________________________

            CreateMap<RoleRequestDto, Role_Model>();
            
            
            CreateMap<RegisterRequestDto, User_Model>();


            CreateMap<User_Model, RegisterRequestDto>();






        }



    }
}
