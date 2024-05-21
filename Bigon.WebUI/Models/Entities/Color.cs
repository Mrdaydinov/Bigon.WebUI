namespace Bigon.WebUI.Models.Entities
{
    public class Color : BaseEntity<int>
    {
        public string Name { get; set; }
        public string HexCode { get; set; }
    }
}
