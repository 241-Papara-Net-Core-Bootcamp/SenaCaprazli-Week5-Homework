using AutoMapper;
using Hangfire;
using PaparaThirdWeek.Data.Abstracts;
using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _user;
        private readonly ICacheService _cacheService; // readonly constructorda değiştirilebilir değeri
        private const string cacheKey = "UserCacheKey"; //const olursa değeri değiştirilemez
        private readonly IMapper _mapper;

        public UserService(IRepository<User> user, ICacheService cacheService, IMapper mapper)
        {
            _user = user;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public void Add(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
           _user.Add(user);
            //Refresh Cache
            BackgroundJob.Enqueue(() => RefreshCache());

        }
        public void RefreshCache()
        {
            _cacheService.Remove(cacheKey);
            var cachedList = _user.GetAll();
            _cacheService.Set(cacheKey, cachedList);
        }


        public List<User> GetAllUsers()
        {
            var userList = _user.GetAll().ToList();
            _cacheService.TryGet<User>(cacheKey, out userList);
            return userList;
        }
    }
}
