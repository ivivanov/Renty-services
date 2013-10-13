using Renty.Services.DataTransferObects;
using Renty.Services.Models;
namespace Renty.Services.ModelMappers
{
    public static class UsersMapper
    {
        public static UserModel ToUserModel(User userEntity)
        {
            UserModel userModel = new UserModel()
            {
                ID = userEntity.UserId,
            };

            return userModel;
        }

        public static User ToUserEntity(UserLoginRegisterModel userModel)
        {
            User userEntity = new User()
                {
                    Username = userModel.Username.ToLower(),
                    AuthCode = userModel.AuthCode,
                };

            return userEntity;
        }
    }
}
