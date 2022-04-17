namespace Tamagotchi.Data.Entities
{
    public class AppSetting
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public bool BoolValue { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}