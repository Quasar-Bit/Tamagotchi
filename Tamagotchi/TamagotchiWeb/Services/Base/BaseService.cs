
using System.Diagnostics;
using System.Text;
using TamagotchiWeb.Exceptions;
using TamagotchiWeb.Models;
using Newtonsoft.Json;

namespace TamagotchiWeb.Services.Base
{
    public class BaseService
    {
        protected async Task<Result<TResult>> MakeApiCall<TResult>(string url, HttpMethod method,
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
                        body = JsonConvert.SerializeObject(data, Formatting.Indented);
                        request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                    }

                    request.Headers.Add("Authorization", "Bearer " + Constants.AccessToken);

                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    var response = await httpClient.SendAsync(request).ConfigureAwait(false);
                    stopWatch.Stop();

                    var str = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var resultData = default(TResult);
                    try
                    {
                        if ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300)
                        {
                            resultData = JsonConvert.DeserializeObject<TResult>(str);
                        }
                        else if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
                        {
                            var resultError = JsonConvert.DeserializeObject<Response<string>>(str);
                            throw new WebServiceException(resultError?.errors ?? new List<string>
                                { response.ReasonPhrase });
                        }
                        else
                        {
                            throw new BaseException();
                        }
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