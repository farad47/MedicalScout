using MCPServer.Entities;

namespace MCPServer.Model
{
    public class BloodTest
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal PriceGross { get; set; }
        public string PreparationInfo { get; set; } = "";

        public ICollection<Indication> Indications { get; set; } = new List<Indication>();
    }
}
