using LinkMe.Domain.Contacts;

namespace LinkMe.Web.Areas.Administrators.Models
{
    public class UserModel<TUser, TLoginModel>
        where TUser : IUser
    {
        public TUser User { get; set; }
        public TLoginModel UserLogin { get; set; }
    }
}