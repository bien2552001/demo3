
using System;

namespace DOANV1.Extensions.Features.ShapeData_Get
{
    public class ShapedData
    {
        public ShapedData()
        {
            Entity = new ShapedData_Entities();
        }
        public Guid Id { get; set; }
        public ShapedData_Entities Entity { get; set; }
    }
}
