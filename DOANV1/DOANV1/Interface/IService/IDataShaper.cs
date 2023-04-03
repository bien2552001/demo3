using DOANV1.Extensions.Features.ShapeData_Get;
using System.Collections.Generic;

namespace DOANV1.Interface.IService
{
    public interface IDataShaper<T>
    {
        IEnumerable<ShapedData> ShapeData(IEnumerable<T> entities, string fieldsString);
        ShapedData ShapeData(T entity, string fieldsString);
    }
}
