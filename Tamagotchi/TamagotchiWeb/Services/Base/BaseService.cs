using System.Diagnostics;
using System.Text;
using Tamagotchi.Application.Exceptions;
using TamagotchiWeb.Models;
using Newtonsoft.Json;

namespace TamagotchiWeb.Services.Base
{
    public class BaseService
    {
        protected async Task<Result<TResult>> MakeApiCall<TResult>(string url, HttpMethod method, string token = null,
            object data = null)
            where TResult : class
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage { RequestUri = new Uri(url), Method = method })
                {
                    var body = string.Empty;

                    if (method != HttpMethod.Get)
                    {
                        if(token == null)
                        {
                            var pairs = new[]
                            {
                                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                                new KeyValuePair<string, string>("client_id", Constants.ApiKey),
                                new KeyValuePair<string, string>("client_secret", Constants.ApiSecret)
                            };
                            request.Content = new FormUrlEncodedContent(pairs);
                        }
                        else
                        {
                            body = JsonConvert.SerializeObject(data, Formatting.Indented);
                            request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                        }
                    }
                    else
                        request.Headers.Add("Authorization", "Bearer " + token);

                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    var response = await httpClient.SendAsync(request).ConfigureAwait(false);
                    stopWatch.Stop();

                    var str = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var resultData = default(TResult);
                    try
                    {
                        if ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300)
                            resultData = JsonConvert.DeserializeObject<TResult>(str);
                        else if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
                        {
                            var resultError = JsonConvert.DeserializeObject<Response<string>>(str);
                            if (str.Contains("Unauthorized"))
                                throw new WebServiceException(new List<string>
                                    { response?.ReasonPhrase });
                            else
                                throw new WebServiceException(resultError?.errors);
                        }
                        else
                            throw new BaseException();
                    }
                    catch (WebServiceException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    return new Result<TResult>
                    {
                        Data = resultData,
                        Status = response.StatusCode
                    };
                }
            }
        }
    }
}