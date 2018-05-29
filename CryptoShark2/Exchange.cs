using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;
using System.Net.Http;

namespace CryptoShark2
{

    #region Exchange Interface / Base Class

    public interface IExchange
    {
        Dictionary<string, MarketInfo> Markets { get; }
        Dictionary<string, CoinInfo> CoinInfo { get; }
        Dictionary<string, SummaryInfo> Summary { get; }
        Dictionary<string, BookInfo> OrderBooks { get; }
        BookInfo GetOrderBook(string pair, string type = "both");
        bool Refresh();
        void PostRefresh();
    }

    public abstract class Exchange<M, C, S, MD, CD, SD> : IExchange
    {
        protected string _PublicKey;
        protected string _PrivateKey;
        protected RequestListMessage<M, MD, Dictionary<string, MarketInfo>> _Markets;
        protected RequestListMessage<C, CD, Dictionary<string, CoinInfo>> _CoinInfo;
        protected RequestListMessage<S, SD, Dictionary<string, SummaryInfo>> _Summary;
        protected Dictionary<string, BookInfo> _OrderBooks = new Dictionary<string, BookInfo>();

        public Dictionary<string, MarketInfo> Markets { get { return _Markets.Data; } }
        public Dictionary<string, CoinInfo> CoinInfo { get { return _CoinInfo.Data; } }
        public Dictionary<string, SummaryInfo> Summary { get { return _Summary.Data; } }
        public Dictionary<string, BookInfo> OrderBooks { get { return _OrderBooks; } }

        public Exchange(string publicKey = null, string privateKey = null)
        {
            _PublicKey = publicKey;
            _PrivateKey = privateKey;
            Initialize();
        }

        public abstract bool Initialize();

        public bool Refresh()
        {
            _Markets.Initialize();
            _CoinInfo.Initialize();
            _Summary.Initialize();

            PostRefresh();

            return true;
        }

        public abstract void PostRefresh();

        public abstract BookInfo GetOrderBook(string pair, string type = "both");

    }

    #endregion

    #region Message Interfaces / Base Classes

    public interface IListDictionaryResponse<T, R> : IResponseBase<T, R>
    {
        List<Dictionary<string, R>> Data { get; set; }
    }

    public interface IDictionaryResponse<T, R> : IResponseBase<T, R>
    {
        Dictionary<string, R> Data { get; set; }
    }

    public interface IListResponse<T, R> : IResponseBase<T, R>
    {
        List<R> Data { get; set; }
    }

    public interface IResponse<T, R> : IResponseBase<T, R>
    {
        R Data { get; set; }
    }

    public interface IResponseBase<T, R>
    {
        bool Success { get; set; }
        string Message { get; set; }
        string ToJson();
        T FromJson(string jsonString);
    }

    public abstract class RequestListDictionaryMessage<T, R, D> : RequestMessageBase<T, R, D>
    {
        public RequestListDictionaryMessage(string url, string[] args = null) : base(url, args) { }

        public abstract void Process(IListDictionaryResponse<T, R> mr);

        public override bool Initialize(string jsonString = null)
        {
            if (jsonString == null && _url != "")
            {
                IListDictionaryResponse<T, R> mr = (IListDictionaryResponse<T, R>)this.Send(_url);
                this._Success = mr.Success;
                this._Message = mr.Message;

                if (this.Success)
                {
                    this.Process(mr);
                }

                return this.Success;
            }
            else
            {
                return false;
            }
        }

    }

    public abstract class RequestDictionaryMessage<T, R, D> : RequestMessageBase<T, R, D>
    {
        public RequestDictionaryMessage(string url, string[] args = null) : base(url, args) { }

        public abstract void Process(IDictionaryResponse<T, R> mr);

        public override bool Initialize(string jsonString = null)
        {
            if (jsonString == null)
            {
                IDictionaryResponse<T, R> mr = (IDictionaryResponse<T, R>)this.Send(_url);
                this._Success = mr.Success;
                this._Message = mr.Message;

                if (this.Success)
                {
                    this.Process(mr);
                }

                return this.Success;
            }
            else
            {
                return false;
            }
        }

    }

    public abstract class RequestListMessage<T, R, D> : RequestMessageBase<T, R, D>
    {
        public RequestListMessage(string url, string[] args = null) : base(url, args) { }

        public abstract void Process(IListResponse<T, R> mr);

        public override bool Initialize(string jsonString = null)
        {
            if (jsonString == null && _url != "")
            {
                IListResponse<T, R> mr = (IListResponse<T, R>)this.Send(_url);
                this._Success = mr.Success;
                this._Message = mr.Message;

                if (this.Success)
                {
                    this.Process(mr);
                }

                return this.Success;
            }
            else
            {
                return false;
            }
        }

    }

    public abstract class RequestMessage<T, R, D> : RequestMessageBase<T, R, D>
    {
        public RequestMessage(string url, string[] args = null) : base(url, args) { }

        public abstract void Process(IResponse<T, R> mr);

        public override bool Initialize(string jsonString = null)
        {
            if (jsonString == null)
            {
                IResponse<T, R> mr = (IResponse<T, R>)this.Send(_url);
                this._Success = mr.Success;
                this._Message = mr.Message;

                if (this.Success)
                {
                    this.Process(mr);
                }

                return this.Success;
            }
            else
            {
                return false;
            }
        }

    }

    public abstract class RequestMessageBase<T, R, D>
    {
        protected D _Data;
        protected bool _Success;
        protected string _Message;
        protected string _url;
        protected string[] _args;
        protected bool _needShim = false;
        protected string _shimStart;
        protected string _shimEnd;
        protected bool _replaceHeaders = false;
        protected string _replaceFrom = "";
        protected string _replaceTo = "";
        public D Data { get { return _Data; } }
        public bool Success { get { return _Success; } }
        public string Message { get { return _Message; } }
        public string[] Args { get { return _args; } }

        public RequestMessageBase(string url, string[] args = null)
        {
            _url = url;
            _args = args;
        }

        public string Shim(string jsonString) { return _shimStart + jsonString + _shimEnd; }
        public abstract bool Initialize(string jsonString = null);

        /*
        public bool Initialize(string jsonString = null)
        {
            if (jsonString == null)
            {
                IResponse<T, R> mr = (IResponse<T, R>)this.Send(_url);
                this._Success = mr.Success;
                this._Message = mr.Message;

                if (this.Success)
                {
                    this.Process(mr);
                }

                return this.Success;
            }
            else
            {
                return false;
            }
        }
        */

        public T Send(string url)
        {
            if (_args != null)
            {
                for (int i = 0; i < _args.Count(); i++)
                {
                    url = url.Replace("[" + i.ToString() + "]", _args[i]);
                }
            }

            string jsonString;

            using (var client = new WebClient())
            {
                jsonString = client.DownloadString(url);
            }

            if (_needShim)
            {
                jsonString = this.Shim(jsonString);
            }

            if (_replaceHeaders)
            {
                jsonString = jsonString.Replace(_replaceFrom, _replaceTo);
            }

            object[] ob = { jsonString };
            Type type = typeof(T);
            T summary = (T)Activator.CreateInstance(type);
            T o = (T)summary.GetType().InvokeMember("FromJson", BindingFlags.InvokeMethod, null, summary, ob);
            PropertyInfo _success = summary.GetType().GetProperty("Success");
            PropertyInfo _message = summary.GetType().GetProperty("Message");

            this._Success = (bool)_success.GetValue(o);
            this._Message = (string)_message.GetValue(o);

            return o;
        }
    }

    #region Unfinished Private API Stuff

    public abstract class PrivateRequestDictionaryMessage<T, R, D>
    {
        protected Dictionary<string, D> _Data = new Dictionary<string, D>();
        protected bool _Success;
        protected string _Message;
        protected bool _needShim = false;
        protected string _shimStart;
        protected string _shimEnd;
        protected bool _listDictionary = false;
        protected bool _IsPrivate = false;
        private int _Nonce = -1;
        private Dictionary<string, string> _PostData;
        private WebClient _Client = new WebClient();
        private string _RequestParams;

        public Dictionary<string, D> Data { get { return _Data; } }
        public bool Success { get { return _Success; } }
        public string Message { get { return _Message; } }
        public object Parent { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string BaseUrl { get; set; }
        public string Signature { get; set; }
        public int Nonce { get { return _Nonce; } }
        public string RequestParams { get { return _RequestParams; } }
        public Dictionary<string, string> PostData { get { return _PostData; } }
        public WebClient Client { get { return _Client; } }
        public bool IsPrivate => (PublicKey == null && PrivateKey == null) ? false : true;

        public PrivateRequestDictionaryMessage(string baseUrl, Dictionary<string, string> postData = null, string publicKey = null, string privateKey = null)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
            BaseUrl = baseUrl;

            if (postData == null)
            {
                _PostData = new Dictionary<string, string>();
            }
            else
            {
                _PostData = postData;
            }

            this._RequestParams = GetRequestParams();
        }

        public abstract string GenerateSignature();
        public abstract string GenerateHeaders();
        public abstract string PrepareSend();

        private string GetRequestParams()
        {
            return JsonConvert.SerializeObject(this._PostData);
        }

        public string GenerateNonce()
        {
            this._Nonce += 1;
            return this.Nonce.ToString() + "." + DateTime.UtcNow.Ticks.ToString();
        }

        public abstract void Process(IListResponse<T, R> mr);
        public abstract void Process(IListDictionaryResponse<T, R> mr);
        public string Shim(string jsonString) { return _shimStart + jsonString + _shimEnd; }
        public bool Initialize(string jsonString = null)
        {
            if (jsonString == null && this.BaseUrl != "")
            {
                if (!_listDictionary)
                {
                    IListResponse<T, R> mr = (IListResponse<T, R>)this.Send();
                    this._Success = mr.Success;
                    this._Message = mr.Message;

                    if (this.Success)
                    {
                        this.Process(mr);
                    }

                }
                else
                {
                    IListDictionaryResponse<T, R> mr = (IListDictionaryResponse<T, R>)this.Send();
                    this._Success = mr.Success;
                    this._Message = mr.Message;

                    if (this.Success)
                    {
                        this.Process(mr);
                    }
                }

                return this.Success;
            }
            else
            {
                return false;
            }
        }

        public T Send()
        {
            string jsonString = this.PrepareSend();

            if (_needShim)
            {
                jsonString = this.Shim(jsonString);
            }

            object[] ob = { jsonString };
            Type type = typeof(T);
            T summary = (T)Activator.CreateInstance(type);
            T o = (T)summary.GetType().InvokeMember("FromJson", BindingFlags.InvokeMethod, null, summary, ob);
            PropertyInfo _success = summary.GetType().GetProperty("Success");
            PropertyInfo _message = summary.GetType().GetProperty("Message");

            this._Success = (bool)_success.GetValue(o);
            this._Message = (string)_message.GetValue(o);

            return o;
        }
    }

    public abstract class IApiRequest
    {
        private int _Nonce = -1;
        private Dictionary<string, string> _PostData;
        private WebClient _Client = new WebClient();
        private string _RequestParams;

        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string BaseUrl { get; set; }
        public string Signature { get; set; }
        public int Nonce { get { return _Nonce; } }
        public string RequestParams { get { return _RequestParams; } }
        public Dictionary<string, string> PostData { get { return _PostData; } }
        public WebClient Client { get { return _Client; } }
        public bool IsPrivate => (PublicKey == null && PrivateKey == null) ? false : true;

        public IApiRequest(string baseUrl, Dictionary<string, string> postData = null, string publicKey = null, string privateKey = null)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
            BaseUrl = baseUrl;

            if (postData == null)
            {
                _PostData = new Dictionary<string, string>();
            }
            else
            {
                _PostData = postData;
            }

            this._RequestParams = GetRequestParams();
        }

        public abstract string GenerateSignature();
        public abstract string GenerateHeaders();
        public abstract string Send();

        private string GetRequestParams()
        {
            return JsonConvert.SerializeObject(this._PostData);
        }

        public string GenerateNonce()
        {
            this._Nonce += 1;
            return this.Nonce.ToString() + "." + DateTime.UtcNow.Ticks.ToString();
        }

        /*
        public string GenerateSignature2()
        {

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] keyBytes = encoding.GetBytes(this.PrivateKey);
            string message = this.BaseUrl;

            foreach(KeyValuePair<string, string> kv in this.Params) {
                message = message.Replace("[" + kv.Key + "]", kv.Value);
            }

            this.TargetUrl = message;
            byte[] messageBytes = encoding.GetBytes(message);
            System.Security.Cryptography.HMACSHA256 cryptographer = new System.Security.Cryptography.HMACSHA256(keyBytes);
            byte[] bytes = cryptographer.ComputeHash(messageBytes);
            this.Signature = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            return this.Signature;
        }
        */
    }

    public abstract class PrivateRequestMessage<T, R, D>
    {
        protected D _Data;
        protected bool _Success;
        protected string _Message;
        protected bool _needShim = false;
        protected string _shimStart;
        protected string _shimEnd;
        private int _Nonce = -1;
        private Dictionary<string, string> _PostData;
        private WebClient _Client = new WebClient();
        private string _RequestParams;

        public D Data { get { return _Data; } }
        public bool Success { get { return _Success; } }
        public string Message { get { return _Message; } }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string BaseUrl { get; set; }
        public string Signature { get; set; }
        public int Nonce { get { return _Nonce; } }
        public string RequestParams { get { return _RequestParams; } }
        public Dictionary<string, string> PostData { get { return _PostData; } }
        public WebClient Client { get { return _Client; } }
        public bool IsPrivate => (PublicKey == null && PrivateKey == null) ? false : true;

        public PrivateRequestMessage(string baseUrl, Dictionary<string, string> postData = null, string publicKey = null, string privateKey = null)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
            BaseUrl = baseUrl;

            if (postData == null)
            {
                _PostData = new Dictionary<string, string>();
            }
            else
            {
                _PostData = postData;
            }

            this._RequestParams = GetRequestParams();
        }

        public abstract string GenerateSignature();
        public abstract string GenerateHeaders();
        public abstract string PrepareSend();

        private string GetRequestParams()
        {
            return JsonConvert.SerializeObject(this._PostData);
        }

        public string GenerateNonce()
        {
            this._Nonce += 1;
            return this.Nonce.ToString() + "." + DateTime.UtcNow.Ticks.ToString();
        }

        public abstract void Process(IResponse<T, R> mr);
        public string Shim(string jsonString) { return _shimStart + jsonString + _shimEnd; }
        public bool Initialize(string jsonString = null)
        {
            if (jsonString == null)
            {
                IResponse<T, R> mr = (IResponse<T, R>)this.Send();
                this._Success = mr.Success;
                this._Message = mr.Message;

                if (this.Success)
                {
                    this.Process(mr);
                }

                return this.Success;
            }
            else
            {
                return false;
            }
        }

        public T Send()
        {
            string jsonString = this.PrepareSend();

            if (_needShim)
            {
                jsonString = this.Shim(jsonString);
            }

            object[] ob = { jsonString };
            Type type = typeof(T);
            T summary = (T)Activator.CreateInstance(type);
            T o = (T)summary.GetType().InvokeMember("FromJson", BindingFlags.InvokeMethod, null, summary, ob);
            PropertyInfo _success = summary.GetType().GetProperty("Success");
            PropertyInfo _message = summary.GetType().GetProperty("Message");

            this._Success = (bool)_success.GetValue(o);
            this._Message = (string)_message.GetValue(o);

            return o;
        }

    }

    #endregion

    #endregion

    #region JSON Converters

    public class JsonCustomConverter : JsonConverter
    {

        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };

        public override bool CanWrite { get { return false; } }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = "";

            if (reader.Value != null)
            {
                value = reader.Value.ToString().ToLower().Trim();
            }

            if (objectType == typeof(double))
            {

                switch (value)
                {
                    case "null":
                    case "":
                        return -1.0;
                }
                return Convert.ToDouble(value);
            }

            if (objectType == typeof(int))
            {
                switch (value)
                {
                    case "null":
                    case "":
                        return 0;
                }
                return Convert.ToInt32(value);
            }

            if (objectType == typeof(Boolean))
            {
                switch (value)
                {
                    case "true":
                    case "yes":
                    case "y":
                    case "1":
                        return true;
                }
                return false;
            }

            return Convert.ToDouble(value);
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(double) || objectType == typeof(int) || objectType == typeof(Boolean))
            {
                return true;
            }
            return false;
        }
    }

    public class JsonDefaultConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }

    #endregion

}
