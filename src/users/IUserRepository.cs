using System;
using System.Net;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;

namespace SimpleMDB;

public interface IUserRepository
{
    public Task<PagedResult<User>> ReadAll(int page, int size);
    public Task<User?> Create(User user);
    public Task<User?> Read(int id);
    public Task<User?> Update(int id, User newUser);
    public Task<User?> Delete(int id);
}
