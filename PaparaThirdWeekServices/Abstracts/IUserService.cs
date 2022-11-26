using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Services.Abstracts
{
  public interface IUserService
    {
        void Add(UserDto user);
        List<User> GetAllUsers();
    }
}
