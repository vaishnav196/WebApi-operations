using WebApi.Models;

namespace WebApi.Repo
{
    public interface UserRepo
    {
        void SignUp(User us);
        //void SignIn(Login log);
    }
}
