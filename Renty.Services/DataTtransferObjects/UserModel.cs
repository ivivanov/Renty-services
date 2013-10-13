using System.Runtime.Serialization;

namespace Renty.Services.DataTransferObects
{
    [DataContract]
    public class UserModel
    {
        [DataMember(Name = "id")]
        public int ID { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }
    }
}
