using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System.Threading.Tasks;
using System.Net.Http;
using Land.Models;
using System.Net.Http.Headers;

namespace Land.Service
{
    public class APIService
    {
        public async Task<Models.Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Models.Response
                {
                    IsSucced = false,
                    Message = "Please turn on your internet settings",
                };

            }

            var isReacheble = await CrossConnectivity.Current.IsRemoteReachable("www.google.com");
            if (!isReacheble)
            {
                return new Models.Response
                {
                    IsSucced = false,
                    Message = "Check your internet connection",
                };
            }
            return new Models.Response
            {
                IsSucced = true,
                Message = "OK",
            };
        }


        public async Task<Models.TokenResponse> GetToken(string urlBase, string username, string password)
        {
            try
            {
                var Client = new HttpClient();
                Client.BaseAddress = new Uri(urlBase);
                var response = await Client.PostAsync("Token", new StringContent(string.Format("grant_type=password&username={0}&password={1}",
                    username, password), Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resultJson = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Models.TokenResponse>(resultJson);
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<Response> Get<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            int id)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    id);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSucced = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSucced = true,
                    Message = "Ok",
                    Result = model,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSucced = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSucced = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSucced = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSucced = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSucced = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSucced = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSucced = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            int id)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    id);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSucced = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSucced = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSucced = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Post<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request, Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSucced = false;
                    return error;
                }

                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSucced = true,
                    Message = "Record added OK",
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSucced = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Post<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSucced = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSucced = true,
                    Message = "Record added OK",
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSucced = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Put<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request,
                    Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    model.GetHashCode());
                var response = await client.PutAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSucced = false;
                    return error;
                }

                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSucced = true,
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSucced = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Delete<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    model.GetHashCode());
                var response = await client.DeleteAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSucced = false;
                    return error;
                }

                return new Response
                {
                    IsSucced = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSucced = false,
                    Message = ex.Message,
                };
            }
        }



    }
}
