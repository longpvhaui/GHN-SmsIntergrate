namespace GHN_SmsIntergrate.Models
{
    public class OrderStatusRequest
    {
        public int CODAmount { get; set; }
        public string? CODTransferDate { get; set; }
        public string? ClientOrderCode { get; set; }
        public int ConvertedWeight { get; set; }
        public string? Description { get; set; }
        public Fee? Fee { get; set; }
        public int Height { get; set; }
        public string? IsPartialReturn { get; set; }
        public int Length { get; set; }
        public string OrderCode { get; set; }
        public int PaymentType { get; set; }
        public string? Reason { get; set; }
        public string? ReasonCode { get; set; }
        public int ShopID { get; set; }
        public string? Status { get; set; }
        public DateTime Time { get; set; }
        public int TotalFee { get; set; }
        public string? Type { get; set; }
        public string? Warehouse { get; set; }
        public int Weight { get; set; }
        public int Width { get; set; }
    }
}


    public class Fee 
        {
            public int Coupon { get; set; }
            public int Insurance { get; set; }
            public int MainService { get; set; }
            public int R2S { get; set; }
            public int Return { get; set; }
            public int StationDO { get; set; }
            public int StationPU { get; set; }
        }
