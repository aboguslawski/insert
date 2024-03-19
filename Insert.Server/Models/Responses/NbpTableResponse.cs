namespace Insert.Server.Models.Responses
{
    public class NbpTableResponse
    {
        public string Table { get; set; }
        public string No { get; set; }
        public DateOnly EffectiveDate { get; set; }
        public NbpRates[] Rates { get; set; }
    }
}
