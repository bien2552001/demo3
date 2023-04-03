using System.Collections.Generic;

namespace DOANV1.Extensions.Features.HATEOAS.HATEOAS_Models
{
    public class LinkCollectionWrapper<T> : LinkResourceBase
    {
        public List<T> Value { get; set; } = new List<T>();
        public LinkCollectionWrapper()
        { }
        public LinkCollectionWrapper(List<T> value)
        {
            Value = value;
        }
    }
}
