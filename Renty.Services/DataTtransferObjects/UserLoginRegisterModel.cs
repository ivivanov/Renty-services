using System.Runtime.Serialization;

namespace Renty.Services.DataTransferObects
{
    [DataContract]
    public class UserLoginRegisterModel
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "authCode")]
        public string AuthCode { get; set; }
    }
}
