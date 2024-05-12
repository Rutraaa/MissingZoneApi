using MissingZoneApi.Contracts.AuthReg;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Repo;

public class AdminRepo : IAdmin
{
    public readonly mzonedbContext _mzonedbContext;

    public AdminRepo(mzonedbContext mzonedbContext)
    {
        _mzonedbContext = mzonedbContext;
    }

    public LoginResult CheckIsExist(LoginRequest userLogin)
    {
        try
        {
            var isExist = _mzonedbContext.Admins
                .Any(item => item.Email == userLogin.Email);
            if (!isExist) return new LoginResult { IsExist = isExist, Messsage = string.Empty };
            isExist = _mzonedbContext.Admins
                .Any(item => item.Password == userLogin.Password);
            if (!isExist) return new LoginResult { IsExist = isExist, Messsage = "Wrong password" };

            return new LoginResult { IsExist = isExist, Messsage = string.Empty };
            ;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}