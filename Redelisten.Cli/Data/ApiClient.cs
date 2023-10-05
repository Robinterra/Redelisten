using System.Text;
using System.Text.Json;

using System.Net;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using ApiService;

namespace MyApiService
{
    public class MyApiClient : IApiClient
    {

        #region vars

        private Uri baseadress;

        private Func<HttpRequestMessage, System.Security.Cryptography.X509Certificates.X509Certificate2, System.Security.Cryptography.X509Certificates.X509Chain, System.Net.Security.SslPolicyErrors, bool>? certificateCheck;
        private string? version;
        public HttpClient client;

        private string? apiKey;

        private event CheckZertifkat? checkCertEasy;

        private event HTTPStatuscode? httpCode;

        private event WsApiEvent? beforeConnectToWebservice;

        #endregion vars

        #region delegate

        public delegate bool CheckZertifkat(string hostname, System.Security.Cryptography.X509Certificates.X509Certificate2 x509Certificate2, System.Security.Cryptography.X509Certificates.X509Chain x509Chain);

        public delegate bool HTTPStatuscode(HttpStatusCode code);

        public delegate bool WsApiEvent(IApiClient api);

        #endregion delegate

        #region get/set

        public event CheckZertifkat CheckCertEasy
        {
            add
            {
                this.checkCertEasy += value;
            }
            remove
            {
                this.checkCertEasy -= value;
            }
        }

        public event HTTPStatuscode HttpCode
        {
            add
            {
                this.httpCode += value;
            }
            remove
            {
                this.httpCode -= value;
            }
        }

        public event WsApiEvent BeforeConnectToWebservice
        {
            add
            {
                this.beforeConnectToWebservice += value;
            }
            remove
            {
                this.beforeConnectToWebservice -= value;
            }
        }

        public string BaseAdress
        {
            get
            {
                return this.baseadress.ToString();
            }
            set
            {
                this.baseadress = new Uri(value);

                this.client = this.BuildHttpClient();
            }
        }

        public string ApiPath
        {
            get;
            set;
        }

        public Func<HttpRequestMessage, System.Security.Cryptography.X509Certificates.X509Certificate2, System.Security.Cryptography.X509Certificates.X509Chain, System.Net.Security.SslPolicyErrors, bool>? CheckCertificate
        {
            get
            {
                return this.certificateCheck;
            }
            set
            {
                this.certificateCheck = value;

                this.client = this.BuildHttpClient();
            }
        }

        private AuthenticationHeaderValue? Authorization
        {
            get;
            set;
        }

        public string? Version
        {
            get
            {
                return this.version;
            }
            set
            {
                this.version = value;

                this.client = this.BuildHttpClient();
            }
        }

        public string? ApiKey
        {
            get
            {
                return this.apiKey;
            }
            set
            {
                this.apiKey = value;

                this.client = this.BuildHttpClient();
            }
        }

        #endregion get/set

        #region ctor

        public MyApiClient(string baseAdress, string apiPath = "")
        {
            this.baseadress = new Uri(baseAdress);
            this.ApiPath = apiPath;

            this.client = this.BuildHttpClient();
        }

        ~MyApiClient()
        {
            this.client.Dispose();
        }

        #endregion

        #region methods

        public bool SetNewAuth(byte[] token, byte[] hashpassword, string mode = "Token")
        {
            return this.SetNewAuth(Convert.ToBase64String(token), Convert.ToBase64String(hashpassword), mode);
        }

        public bool SetNewAuth(string username, string password, string mode = "Basic")
        {
            byte[] byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");

            this.Authorization = new AuthenticationHeaderValue(mode, Convert.ToBase64String(byteArray));

            this.client.DefaultRequestHeaders.Authorization = this.Authorization;

            return true;
        }

        public bool SetNewAuth(string value, string mode = "Bearer")
        {
            this.Authorization = new AuthenticationHeaderValue(mode, value);

            this.client.DefaultRequestHeaders.Authorization = this.Authorization;

            return true;
        }

        public Task<HttpResponseMessage> SendManuelRequest(HttpRequestMessage request)
        {
            return this.client.SendAsync(request);
        }

        private bool CheckCert(HttpRequestMessage? httpRequestMessage, System.Security.Cryptography.X509Certificates.X509Certificate2 x509Certificate2, System.Security.Cryptography.X509Certificates.X509Chain x509Chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            if (this.checkCertEasy == null) return false;

            string host = string.Empty;

            if ( httpRequestMessage != null )
            {
                if ( httpRequestMessage.RequestUri != null ) host = httpRequestMessage.RequestUri.Host;
            }

            return this.checkCertEasy(host, x509Certificate2, x509Chain);
        }

        private HttpClientHandler BuildHandler()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback = this.CheckCert!;

            if (this.CheckCertificate == null) return httpClientHandler;

            httpClientHandler.ServerCertificateCustomValidationCallback = this.CheckCertificate!;

            return httpClientHandler;
        }

        private HttpClient BuildHttpClient()
        {
            HttpClientHandler handler = this.BuildHandler();

            HttpClient client = new HttpClient(handler);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (this.Authorization != null) client.DefaultRequestHeaders.Authorization = this.Authorization;

            client.BaseAddress = this.baseadress;

            if (this.version != null) client.DefaultRequestHeaders.Add("X-Version", this.version);

            if (this.apiKey != null) client.DefaultRequestHeaders.Add("X-api-key", this.apiKey);

            return client;
        }

        private string GetQuerryFromParams(params object[] header)
        {
            string query = this.ApiPath is null ? string.Empty : this.ApiPath;

            return $"{query}/{string.Join('/', header)}";
        }

        private async Task<TResponse?> GetResult<TResponse>(HttpResponseMessage httpResponse) where TResponse : new()
        {
            if (this.httpCode != null) this.httpCode(httpResponse.StatusCode);

            TResponse? result;
            try
            {
                result = await httpResponse.Content.ReadFromJsonAsync<TResponse>();
            }
            catch (Exception e)
            {
                return this.BuildApiResponse<TResponse>(isSuccess: false, status: "error_deserializing", exception: e);
            }

            if (result is null) return this.BuildApiResponse<TResponse>(false, httpResponse.StatusCode, httpResponse.StatusCode.ToString());

            return this.SetToResponse(result, httpResponse.IsSuccessStatusCode, httpResponse.StatusCode);
        }

        private TResponse SetToResponse<TResponse>(TResponse result, bool isSuccess, HttpStatusCode statusCode)
        {
            if (result is not IApiResponse apiResponse) return result;

            apiResponse.SetHttpCode((int)statusCode);

            apiResponse.SetIsSuccess(isSuccess);

            return result;
        }

        private TResponse? BuildApiResponse<TResponse>(bool? isSuccess = null, HttpStatusCode? statusCode = null, string? status = null, Exception? exception = null) where TResponse : new()
        {
            TResponse result = new();
            if (result is not IApiResponse apiResponse) return default;

            return (TResponse)this.BuildApiResponse(apiResponse, isSuccess, statusCode, status, exception);
        }

        private TResponse BuildApiResponse<TResponse>(TResponse apiResponse, bool? isSuccess = null, HttpStatusCode? statusCode = null, string? status = null, Exception? exception = null) where TResponse : IApiResponse
        {
            if (isSuccess is not null) apiResponse.SetIsSuccess((bool) isSuccess);

            if (status is not null) apiResponse.SetStatus(status.ToString());

            if (statusCode is not null) apiResponse.SetHttpCode((int)statusCode);

            if (exception is not null) apiResponse.SetException(exception);

            return apiResponse;
        }

        #region PutData

        public async Task<TResponse?> PutAsync<TResponse, TRequest> (TRequest data, string route) where TResponse : new()
        {
            string apiPath = $"{this.ApiPath}/{route}";

            if (this.beforeConnectToWebservice != null) if (!this.beforeConnectToWebservice(this)) return default;

            HttpResponseMessage httpResponse;

            try
            {
                httpResponse = await this.client.PutAsJsonAsync<TRequest>(apiPath, data);
            }
            catch (Exception e)
            {
                return this.BuildApiResponse<TResponse>(isSuccess: false, status: "error_httpRequest", exception: e);
            }

            return await this.GetResult<TResponse>(httpResponse);
        }

        #endregion PutData

        #region PostData

        public async Task<TResponse?> PostAsync<TResponse, TRequest>(TRequest data, string route) where TResponse : new()
        {
            string apiPath = $"{this.ApiPath}/{route}";

            if (this.beforeConnectToWebservice != null) if (!this.beforeConnectToWebservice(this)) return default;

            HttpResponseMessage httpResponse;

            try
            {
                httpResponse = await this.client.PostAsJsonAsync<TRequest>(apiPath, data);
            }
            catch (Exception e)
            {
                return this.BuildApiResponse<TResponse>(isSuccess: false, status: e.ToString(), exception: e);
            }

            return await this.GetResult<TResponse>(httpResponse);
        }

        public async Task<TResponse?> PostAsync<TResponse>(string route) where TResponse : new()
        {
            string apiPath = $"{this.ApiPath}/{route}";

            if (this.beforeConnectToWebservice != null) if (!this.beforeConnectToWebservice(this)) return default;

            HttpResponseMessage httpResponse;

            try
            {
                httpResponse = await this.client.PostAsync(apiPath, null);
            }
            catch (Exception e)
            {
                return this.BuildApiResponse<TResponse>(isSuccess: false, status: e.ToString(), exception: e);
            }

            return await this.GetResult<TResponse>(httpResponse);
        }

        #endregion PostData

        #region GetData

        public async Task<DownloadStreamResponse?> GetDownloadStreamAsync<DownloadStreamResponse>(params object[] header) where DownloadStreamResponse : IApiDownloadStreamResponse, new()
        {
            string query = this.GetQuerryFromParams(header);

            if (this.beforeConnectToWebservice != null) if (!this.beforeConnectToWebservice(this)) return default(DownloadStreamResponse);

            HttpResponseMessage httpResponse;

            try
            {
                httpResponse = await this.client.GetAsync(query, HttpCompletionOption.ResponseHeadersRead);
            }
            catch (Exception e)
            {
                return this.BuildApiResponse<DownloadStreamResponse>(isSuccess: false, status: e.ToString(), exception: e);
            }

            IEnumerable<string>? values;

            httpResponse.Content.Headers.TryGetValues("Content-Type", out values);
            if (values == null) return this.BuildApiResponse<DownloadStreamResponse>(isSuccess: false, status: "content-type not in header");
            //if (values.Any(t=>t.ToLower() == "application/json")) return await this.GetResult<DownloadStreamResponse>(httpResponse);

            Stream stream = await httpResponse.Content.ReadAsStreamAsync();

            DownloadStreamResponse response = new DownloadStreamResponse();
            response.SetStream(stream);

            string? contentType = values.FirstOrDefault();
            if (contentType is not null) response.SetContentType(contentType);

            return this.SetToResponse(response, httpResponse.IsSuccessStatusCode, httpResponse.StatusCode);
        }

        public async IAsyncEnumerable<T?> GetAsyncEnumerable<T>(params object[] header)
        {
            string query = this.GetQuerryFromParams(header);

            Stream? stream = await this.GetStreamFromQueryAsync(query);
            if (stream == null) throw new Exception();

            IAsyncEnumerable<T?> enumarble = JsonSerializer.DeserializeAsyncEnumerable<T>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, DefaultBufferSize = 128 });

            await foreach (T? item in enumarble)
            {
                yield return item;
            }
        }

        private async Task<Stream?> GetStreamFromQueryAsync(string query)
        {
            if (this.beforeConnectToWebservice != null) if (!this.beforeConnectToWebservice(this)) return null;

            HttpResponseMessage httpResponse = await this.client.GetAsync(query, HttpCompletionOption.ResponseHeadersRead);

            if (this.httpCode != null) this.httpCode(httpResponse.StatusCode);

            if (!httpResponse.IsSuccessStatusCode) return null;

            return await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        public Task<TResponse?> GetAsync<TResponse>(params object[] header) where TResponse : new()
        {
            string query = this.GetQuerryFromParams(header);

            return this.GetDataFromQueryAsync<TResponse>(query);
        }

        private async Task<TResponse?> GetDataFromQueryAsync<TResponse>(string query) where TResponse : new()
        {
            if (this.beforeConnectToWebservice != null) if (!this.beforeConnectToWebservice(this)) return default;

            HttpResponseMessage httpResponse;

            try
            {
                httpResponse = await this.client.GetAsync(query, HttpCompletionOption.ResponseHeadersRead);
            }
            catch (Exception e)
            {
                return this.BuildApiResponse<TResponse>(isSuccess: false, status: e.ToString(), exception: e);
            }

            return await this.GetResult<TResponse>(httpResponse);
        }

        #endregion GetData

        #region Delete

        public Task<TResponse?> DeleteAsync<TResponse>(params object[] header) where TResponse : new()
        {
            string query = this.GetQuerryFromParams(header);

            return this.DeleteFromQueryAsync<TResponse>(query);
        }

        private async Task<TResponse?> DeleteFromQueryAsync<TResponse>(string query) where TResponse : new()
        {
            if (this.beforeConnectToWebservice != null) if (!this.beforeConnectToWebservice(this)) return default;

            HttpResponseMessage httpResponse;

            try
            {
                httpResponse = await this.client.DeleteAsync(query);
            }
            catch (Exception e)
            {
                return this.BuildApiResponse<TResponse>(isSuccess: false, status: e.ToString());
            }

            return await this.GetResult<TResponse>(httpResponse);
        }

        #endregion Delete

        public async Task<TResponse> UploadFileAsync<TResponse>(List<FileUploadRequest> files, string route) where TResponse : IApiResponse, new()
        {
            string query = $"{this.ApiPath}/{route}";

            if (this.beforeConnectToWebservice != null) if (!this.beforeConnectToWebservice(this)) return new TResponse();

            MultipartFormDataContent content = new MultipartFormDataContent();

            foreach (FileUploadRequest file in files)
            {
                StreamContent stream = new StreamContent(file.Stream);

                content.Add(stream, file.Name, file.FileName);
            }

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await this.client.PostAsync(query, content);
            }
            catch (Exception e)
            {
                return this.BuildApiResponse<TResponse>(isSuccess: false, status: e.ToString(), exception: e)!;
            }

            return (await this.GetResult<TResponse>(httpResponse))!;
        }
/*
        public B? DownloadFile<T, B>(T data, string route) where B : IResponseFileDownload, new()
        {
            string query = $"{this.ApiPath}/{route}";

            try
            {
                if (this.beforeConnectToWebservice != null) if (!this.beforeConnectToWebservice(this)) return default(B);

                HttpResponseMessage httpResponse = this.client.PostAsJsonAsync<T>(query, data).Result;

                if (this.httpCode != null) this.httpCode(httpResponse.StatusCode);

                if (!httpResponse.IsSuccessStatusCode) return default(B);

                IEnumerable<string>? values;

                httpResponse.Content.Headers.TryGetValues("Content-Type", out values);
                if (values == null) return httpResponse.Content.ReadFromJsonAsync<B>().Result;

                string? value = values.FirstOrDefault();

                if (value != "APPLICATION/octet-stream") return httpResponse.Content.ReadFromJsonAsync<B>().Result;

                B result = new B();

                result.IsOk = true;
                result.Result = httpResponse.Content.ReadAsStreamAsync().Result;

                return result;
            }
            catch
            {
                return default(B);
            }
        }*/

        #endregion methods

    }
}
// -- [EOF] --