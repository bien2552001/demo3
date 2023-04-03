using DOANV1.Extensions.Features.ShapeData_Get;
using SharpCompress.Common;
using System.Collections.Generic;

namespace DOANV1.Extensions.Features.HATEOAS.HATEOAS_Models
{
    public class LinkResponse
    {
        public bool HasLinks { get; set; }
        public List<ShapedData_Entities> ShapedEntities { get; set; }
        public LinkCollectionWrapper<ShapedData_Entities> LinkedEntities { get; set; }
        public LinkResponse()
        {
            LinkedEntities = new LinkCollectionWrapper<ShapedData_Entities>();

            ShapedEntities = new List<ShapedData_Entities>();
        }
    }
}
