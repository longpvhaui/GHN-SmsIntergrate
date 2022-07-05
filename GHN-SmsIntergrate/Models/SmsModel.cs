namespace GHN_SmsIntergrate.Models
{
    public class SmsModel
    {
        public string  UserName { get; set; }
        public string Password { get; set; }
        public string BrandName { get; set; }
        public string SmsContent { get; set; }
        public string? TimeSend { get; set; }  //yyyy-MM-dd HH:mm:ss.
        public List<string> Phones { get; set; } = new List<string>();
        public string ClientId { get; set; }
        public string? CheckSum { get; set; }
    }
}
