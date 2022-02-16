using Core.StaticData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Repository.ApiClient
{
    public class WebApiExecuter : IWebApiExecuter
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;
        private readonly ITokenRepository _tokenRepository;
        public WebApiExecuter(string baseUrl, HttpClient httpClient,ITokenRepository tokenRepository)
        {
            _baseUrl = baseUrl;
            _httpClient = httpClient;
            _tokenRepository = tokenRepository;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Clear();// Очищаем входящие заголовки
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));// устанавливаем заголовок типа ContentTypeHeader   Important!!!
        }

        // Создаём универсальный метод для всех GET запросов
        public async Task<T> InvokeGet<T>(string uri)
        {
            var token = await _tokenRepository.GetToken();
             AddTokenHeader(token);
            var response= await _httpClient.GetFromJsonAsync<T>(GetUrl(uri));
            return response;
        }
        // Создаём универсальный метод для всех POST запросов
        public async Task<T> InvokePost<T>(string uri, T data)
        {
            var token = await _tokenRepository.GetToken();
            AddTokenHeader(token);
            var response = await _httpClient.PostAsJsonAsync(GetUrl(uri), data);
            //response.EnsureSuccessStatusCode();
            await HandleError(response);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task InvokePut<T>(string uri, T data)
        {
            var token = await _tokenRepository.GetToken();
            AddTokenHeader(token);
            var response = await _httpClient.PutAsJsonAsync(GetUrl(uri), data);
            //response.EnsureSuccessStatusCode();
            await HandleError(response);
        }
        public async Task InvokeDelete(string uri)
        {
            var token = await _tokenRepository.GetToken();
            AddTokenHeader(token);
            var response = await _httpClient.DeleteAsync(GetUrl(uri));
            //response.EnsureSuccessStatusCode();
            await HandleError(response);
        }

        private string GetUrl(string uri) => $"{_baseUrl}/{uri}";

        private static async Task HandleError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error);
            }
        }

        public async Task<string?> InvokePostReturnString<T>(string uri, T? data)
        {
            var token = await _tokenRepository.GetToken();
            AddTokenHeader(token);
            var response = await _httpClient.PostAsJsonAsync(GetUrl(uri), data);
            await HandleError(response);
            return await response.Content.ReadAsStringAsync();
        }
        void  AddTokenHeader(string? token)// Добавление токена в заголовок запроса
        {
           if(_tokenRepository is not null && !string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Remove(SD.TOKENHEADER);
                _httpClient.DefaultRequestHeaders.Add(SD.TOKENHEADER,token);
            }
        }
    }
}
