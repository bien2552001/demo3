
using System;

namespace DOANV1.Entities.DTO.DataDto.CurrentDto
{
    public record Post_CurrentDto
    {

        public string Name = "Dòng Điện";
        public double value { get; init; }

        public DateTimeOffset CreatedDate = DateTimeOffset.Now;
    }
}
