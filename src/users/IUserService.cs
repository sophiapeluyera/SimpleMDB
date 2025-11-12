using System;
using System.Net;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;

namespace SimpleMDB
{
    public interface IUserService
    {
        Task<Result<PagedResult<User>>> ReadAll(int page, int size);
        Task<Result<User>> Create(User user);
        Task<Result<User>> Read(int id);
        Task<Result<User>> Update(int id, User newUser);
        Task<Result<User>> Delete(int id);
    }
}
