﻿
namespace MyApp.Repository.ApiClient
{
    public interface IWebApiExecuter
    {
        Task InvokeDelete(string uri);
        Task<T> InvokeGet<T>(string uri);
        Task<T> InvokePost<T>(string uri, T data);
        Task InvokePut<T>(string uri, T data);
        Task<string> InvokePostReturnString<T>(string uri,T data);
    }
}