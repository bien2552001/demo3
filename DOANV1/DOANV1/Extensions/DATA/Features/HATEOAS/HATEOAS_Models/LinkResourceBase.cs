using System.Collections.Generic;

namespace DOANV1.Extensions.Features.HATEOAS.HATEOAS_Models
{
    public class LinkResourceBase
    {
        public LinkResourceBase()
        { }
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
