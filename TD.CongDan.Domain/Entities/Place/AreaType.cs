using TD.Libs.Abstractions.Domain;


namespace TD.CongDan.Domain.Entities
{
    public class AreaType : AuditableEntity
    {
        public string Name { get; set; }
 
        public string Code { get; set; }
        public string Description { get; set; }


    }
}
