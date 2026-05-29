using MCPServer.Model;

namespace MCPServer.Entities
{
    public class Indication
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public ICollection<BloodTest> BloodTests { get; set; } = new List<BloodTest>();
    }
}
