namespace Dashboard.Models
{
    public class Reciepts
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime time { get; set; }
        public InventoryItems items { get; set; }
    }
}
