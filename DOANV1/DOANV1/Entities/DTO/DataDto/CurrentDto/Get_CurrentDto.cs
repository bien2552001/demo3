using System;

namespace DOANV1.Entities.DTO.DataDto.CurrentDto
{
    public record Get_CurrentDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public double value { get; init; }
        public DateTimeOffset CreatedDate { get; init; }

    }
}
