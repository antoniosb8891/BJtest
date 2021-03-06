﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using BJtest.Common.Exceptions;
using BJtest.Common.Managers.UserManager;
using BJtest.Models.RestModels;
using BJtest.REST;
using Xamarin.Forms;
using BJtest.Commons.Helpers;
using System.Diagnostics;
using Xamarin.Essentials;

[assembly: Dependency(typeof(UserManagerImpl))]
namespace BJtest.Common.Managers.UserManager
{
    public class UserManagerImpl : IUserManager
    {
        private readonly INetworkService networkService = DependencyService.Get<INetworkService>();

        public async Task<bool> Login(string login, string password)
        {
            var arg = new LoginRequest()
            {
                UserName = login,
                Password = password,
            };
            var answer = await networkService.PerformNetworkRequest(NetworkService.TaskType.LOGIN, new Dictionary<string, object>
                {
                    { "arg1", arg },
                });

            if (answer != null && answer.IsStatusOk && answer.Data is JObject data)
            {
                var model = data.ToObject<TokenResponse>();
                if (model != null)
                {
                    Settings.Token = model.Token;
                    networkService.Reinitialize(Settings.Token);
                    return true;
                }
            }
            return false;
        }

        public bool IsLogged()
        {
            return !Settings.Token.Equals(Settings.TokenDefault);
        }

        public void ResetLogin()
        {
            Settings.Token = Settings.TokenDefault;
        }
    }
}
