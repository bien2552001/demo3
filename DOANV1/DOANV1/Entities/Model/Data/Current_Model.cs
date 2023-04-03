using System;

namespace DOANV1.Entities.Model.Data
{
    public class Current_Model
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double value { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

    }
}
