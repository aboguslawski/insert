namespace Insert.Server.Models.Entities
{
    public class Currency
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public decimal Mid { get; set; }
        public DateOnly EffectiveDate { get; set; }
        public string Table { get; set; }
    }
}
