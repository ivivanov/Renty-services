using System.Runtime.Serialization;

namespace Renty.Services.DataTransferObects
{
    [DataContract]
    public class UserLoggedModel
    {
        [DataMember(Name = "sessionKey")]
        public string SessionKey { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }
    }
}
