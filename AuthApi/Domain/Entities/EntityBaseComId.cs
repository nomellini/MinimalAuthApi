using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthApi.Domain.Entities
{
    public class EntityBaseComId
    {
        [Key]
        [Column(TypeName = "uuid")]

        public Guid Id { get; set; } 
    }
}
