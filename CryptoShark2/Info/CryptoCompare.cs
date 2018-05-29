// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var welcome = Welcome.FromJson(jsonString);

namespace CryptoShark2.Info.CryptoCompare
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    #region JSON Translators

    public partial class CoinList : IDictionaryResponse<CoinList, CoinListData>
    {
        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("BaseImageUrl")]
        public string BaseImageUrl { get; set; }

        [JsonProperty("BaseLinkUrl")]
        public string BaseLinkUrl { get; set; }

        [JsonProperty("DefaultWatchlist")]
        public DefaultWatchlist DefaultWatchlist { get; set; }

        [JsonProperty("SponosoredNews")]
        public List<SponosoredNew> SponosoredNews { get; set; }

        [JsonProperty("Data")]
        public Dictionary<string, CoinListData> Data { get; set; }

        [JsonProperty("Type")]
        public long Type { get; set; }
    }

    public partial class CoinListData
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Url")]
        public string Url { get; set; }

        [JsonProperty("ImageUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageUrl { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Symbol")]
        public string Symbol { get; set; }

        [JsonProperty("CoinName")]
        public string CoinName { get; set; }

        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("Algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty("ProofType")]
        public ProofType ProofType { get; set; }

        [JsonProperty("FullyPremined")]
        public string FullyPremined { get; set; }

        [JsonProperty("TotalCoinSupply")]
        public string TotalCoinSupply { get; set; }

        [JsonProperty("PreMinedValue")]
        public PreMinedValue PreMinedValue { get; set; }

        [JsonProperty("TotalCoinsFreeFloat")]
        public PreMinedValue TotalCoinsFreeFloat { get; set; }

        [JsonProperty("SortOrder")]
        public string SortOrder { get; set; }

        [JsonProperty("Sponsored")]
        public bool Sponsored { get; set; }

        [JsonProperty("IsTrading")]
        public bool IsTrading { get; set; }
    }

    public partial class DefaultWatchlist
    {
        [JsonProperty("CoinIs")]
        public string CoinIs { get; set; }

        [JsonProperty("Sponsored")]
        public string Sponsored { get; set; }
    }

    public partial class SponosoredNew
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("published_on")]
        public long PublishedOn { get; set; }

        [JsonProperty("imageurl")]
        public string Imageurl { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("categories")]
        public string Categories { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("source_info")]
        public SourceInfo SourceInfo { get; set; }
    }

    public partial class SourceInfo
    {
    }

    public enum PreMinedValue { NA };

    public enum ProofType { DPoR, DPoS, DPoSLPoS, DPoWPoW, LPoS, Lft, MFba, NA, PoA, PoB, PoBPoS, PoBh, PoC, PoI, PoP, PoPPoVPoQ, PoPp, PoS, PoSLPoS, PoSPoB, PoSPoD, PoSPoP, PoSPoW, PoSPoWPoT, PoSign, PoSt, PoW, PoWAndPoS, PoWDPoW, PoWHiPoS, PoWNPoS, PoWPoMPoSii, PoWPoS, PoWPoSPoC, PoWPoW, PoWPoZ, PoWt, PowPoS, PowPoSc, ProofOfOwnership, ProofOfStake, ProofTypePoS, ProofTypePoWPoS, PurplePoWPoS, ScryptAdaptiveNAsicResistant, TPoS, Tangle, The240000000 };

    public partial class CoinList
    {
        public CoinList FromJson(string json) => JsonConvert.DeserializeObject<CoinList>(json, Converter.Settings);
        public string ToJson() => JsonConvert.SerializeObject(this, Converter.Settings);
    }

    public static class CoinListSerialize
    {
        public static string ToJson(this CoinList self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new PreMinedValueConverter(),
                new ProofTypeConverter(),
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class PreMinedValueConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PreMinedValue) || t == typeof(PreMinedValue?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "N/A")
            {
                return PreMinedValue.NA;
            }
            throw new Exception("Cannot unmarshal type PreMinedValue");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (PreMinedValue)untypedValue;
            if (value == PreMinedValue.NA)
            {
                serializer.Serialize(writer, "N/A"); return;
            }
            throw new Exception("Cannot marshal type PreMinedValue");
        }
    }

    internal class ProofTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ProofType) || t == typeof(ProofType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case " PoW/PoS":
                    return ProofType.PurplePoWPoS;
                case "240000000":
                    return ProofType.The240000000;
                case "DPoR":
                    return ProofType.DPoR;
                case "DPoS":
                    return ProofType.DPoS;
                case "DPoS/LPoS":
                    return ProofType.DPoSLPoS;
                case "LFT":
                    return ProofType.Lft;
                case "LPoS":
                    return ProofType.LPoS;
                case "N/A":
                    return ProofType.NA;
                case "POBh":
                    return ProofType.PoBh;
                case "PoA":
                    return ProofType.PoA;
                case "PoB":
                    return ProofType.PoB;
                case "PoB/PoS":
                    return ProofType.PoBPoS;
                case "PoC":
                    return ProofType.PoC;
                case "PoI":
                    return ProofType.PoI;
                case "PoP":
                    return ProofType.PoP;
                case "PoP/PoV/PoQ":
                    return ProofType.PoPPoVPoQ;
                case "PoPP":
                    return ProofType.PoPp;
                case "PoS":
                    return ProofType.PoS;
                case "PoS ":
                    return ProofType.ProofTypePoS;
                case "PoS/LPoS":
                    return ProofType.PoSLPoS;
                case "PoS/PoB":
                    return ProofType.PoSPoB;
                case "PoS/PoD":
                    return ProofType.PoSPoD;
                case "PoS/PoP":
                    return ProofType.PoSPoP;
                case "PoS/PoW":
                    return ProofType.PoSPoW;
                case "PoS/PoW/PoT":
                    return ProofType.PoSPoWPoT;
                case "PoST":
                    return ProofType.PoSt;
                case "PoSign":
                    return ProofType.PoSign;
                case "PoW":
                    return ProofType.PoW;
                case "PoW and PoS":
                    return ProofType.PoWAndPoS;
                case "PoW/DPoW":
                    return ProofType.PoWDPoW;
                case "PoW/HiPoS":
                    return ProofType.PoWHiPoS;
                case "PoW/PoM/PoSII":
                    return ProofType.PoWPoMPoSii;
                case "PoW/PoS":
                    return ProofType.PoWPoS;
                case "PoW/PoS ":
                    return ProofType.ProofTypePoWPoS;
                case "PoW/PoS/PoC":
                    return ProofType.PoWPoSPoC;
                case "PoW/PoW":
                    return ProofType.PoWPoW;
                case "PoW/PoZ":
                    return ProofType.PoWPoZ;
                case "PoW/nPoS":
                    return ProofType.PoWNPoS;
                case "PoWT":
                    return ProofType.PoWt;
                case "Pow/PoS":
                    return ProofType.PowPoS;
                case "Pow/PoSC":
                    return ProofType.PowPoSc;
                case "Proof of Ownership":
                    return ProofType.ProofOfOwnership;
                case "Proof of Stake":
                    return ProofType.ProofOfStake;
                case "Scrypt-adaptive-N (ASIC resistant)":
                    return ProofType.ScryptAdaptiveNAsicResistant;
                case "TPoS":
                    return ProofType.TPoS;
                case "Tangle":
                    return ProofType.Tangle;
                case "dPoW/PoW":
                    return ProofType.DPoWPoW;
                case "mFBA":
                    return ProofType.MFba;
            }
            throw new Exception("Cannot unmarshal type ProofType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ProofType)untypedValue;
            switch (value)
            {
                case ProofType.PurplePoWPoS:
                    serializer.Serialize(writer, " PoW/PoS"); return;
                case ProofType.The240000000:
                    serializer.Serialize(writer, "240000000"); return;
                case ProofType.DPoR:
                    serializer.Serialize(writer, "DPoR"); return;
                case ProofType.DPoS:
                    serializer.Serialize(writer, "DPoS"); return;
                case ProofType.DPoSLPoS:
                    serializer.Serialize(writer, "DPoS/LPoS"); return;
                case ProofType.Lft:
                    serializer.Serialize(writer, "LFT"); return;
                case ProofType.LPoS:
                    serializer.Serialize(writer, "LPoS"); return;
                case ProofType.NA:
                    serializer.Serialize(writer, "N/A"); return;
                case ProofType.PoBh:
                    serializer.Serialize(writer, "POBh"); return;
                case ProofType.PoA:
                    serializer.Serialize(writer, "PoA"); return;
                case ProofType.PoB:
                    serializer.Serialize(writer, "PoB"); return;
                case ProofType.PoBPoS:
                    serializer.Serialize(writer, "PoB/PoS"); return;
                case ProofType.PoC:
                    serializer.Serialize(writer, "PoC"); return;
                case ProofType.PoI:
                    serializer.Serialize(writer, "PoI"); return;
                case ProofType.PoP:
                    serializer.Serialize(writer, "PoP"); return;
                case ProofType.PoPPoVPoQ:
                    serializer.Serialize(writer, "PoP/PoV/PoQ"); return;
                case ProofType.PoPp:
                    serializer.Serialize(writer, "PoPP"); return;
                case ProofType.PoS:
                    serializer.Serialize(writer, "PoS"); return;
                case ProofType.ProofTypePoS:
                    serializer.Serialize(writer, "PoS "); return;
                case ProofType.PoSLPoS:
                    serializer.Serialize(writer, "PoS/LPoS"); return;
                case ProofType.PoSPoB:
                    serializer.Serialize(writer, "PoS/PoB"); return;
                case ProofType.PoSPoD:
                    serializer.Serialize(writer, "PoS/PoD"); return;
                case ProofType.PoSPoP:
                    serializer.Serialize(writer, "PoS/PoP"); return;
                case ProofType.PoSPoW:
                    serializer.Serialize(writer, "PoS/PoW"); return;
                case ProofType.PoSPoWPoT:
                    serializer.Serialize(writer, "PoS/PoW/PoT"); return;
                case ProofType.PoSt:
                    serializer.Serialize(writer, "PoST"); return;
                case ProofType.PoSign:
                    serializer.Serialize(writer, "PoSign"); return;
                case ProofType.PoW:
                    serializer.Serialize(writer, "PoW"); return;
                case ProofType.PoWAndPoS:
                    serializer.Serialize(writer, "PoW and PoS"); return;
                case ProofType.PoWDPoW:
                    serializer.Serialize(writer, "PoW/DPoW"); return;
                case ProofType.PoWHiPoS:
                    serializer.Serialize(writer, "PoW/HiPoS"); return;
                case ProofType.PoWPoMPoSii:
                    serializer.Serialize(writer, "PoW/PoM/PoSII"); return;
                case ProofType.PoWPoS:
                    serializer.Serialize(writer, "PoW/PoS"); return;
                case ProofType.ProofTypePoWPoS:
                    serializer.Serialize(writer, "PoW/PoS "); return;
                case ProofType.PoWPoSPoC:
                    serializer.Serialize(writer, "PoW/PoS/PoC"); return;
                case ProofType.PoWPoW:
                    serializer.Serialize(writer, "PoW/PoW"); return;
                case ProofType.PoWPoZ:
                    serializer.Serialize(writer, "PoW/PoZ"); return;
                case ProofType.PoWNPoS:
                    serializer.Serialize(writer, "PoW/nPoS"); return;
                case ProofType.PoWt:
                    serializer.Serialize(writer, "PoWT"); return;
                case ProofType.PowPoS:
                    serializer.Serialize(writer, "Pow/PoS"); return;
                case ProofType.PowPoSc:
                    serializer.Serialize(writer, "Pow/PoSC"); return;
                case ProofType.ProofOfOwnership:
                    serializer.Serialize(writer, "Proof of Ownership"); return;
                case ProofType.ProofOfStake:
                    serializer.Serialize(writer, "Proof of Stake"); return;
                case ProofType.ScryptAdaptiveNAsicResistant:
                    serializer.Serialize(writer, "Scrypt-adaptive-N (ASIC resistant)"); return;
                case ProofType.TPoS:
                    serializer.Serialize(writer, "TPoS"); return;
                case ProofType.Tangle:
                    serializer.Serialize(writer, "Tangle"); return;
                case ProofType.DPoWPoW:
                    serializer.Serialize(writer, "dPoW/PoW"); return;
                case ProofType.MFba:
                    serializer.Serialize(writer, "mFBA"); return;
            }
            throw new Exception("Cannot marshal type ProofType");
        }
    }

    public partial class CoinFullInfo : IResponse<CoinFullInfo, CoinFullInfoData>
    {
        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Data")]
        public CoinFullInfoData Data { get; set; }

        [JsonProperty("Type")]
        public long Type { get; set; }
    }

    public partial class CoinFullInfoData
    {
        [JsonProperty("SEO")]
        public Seo Seo { get; set; }

        [JsonProperty("General")]
        public General General { get; set; }

        [JsonProperty("ICO")]
        public Ico Ico { get; set; }

        [JsonProperty("Subs")]
        public List<string> Subs { get; set; }

        [JsonProperty("StreamerDataRaw")]
        public List<string> StreamerDataRaw { get; set; }
    }

    public partial class General
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("DocumentType")]
        public string DocumentType { get; set; }

        [JsonProperty("H1Text")]
        public string H1Text { get; set; }

        [JsonProperty("DangerTop")]
        public string DangerTop { get; set; }

        [JsonProperty("WarningTop")]
        public string WarningTop { get; set; }

        [JsonProperty("InfoTop")]
        public string InfoTop { get; set; }

        [JsonProperty("Symbol")]
        public string Symbol { get; set; }

        [JsonProperty("Url")]
        public string Url { get; set; }

        [JsonProperty("BaseAngularUrl")]
        public string BaseAngularUrl { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ImageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Features")]
        public string Features { get; set; }

        [JsonProperty("Technology")]
        public string Technology { get; set; }

        [JsonProperty("TotalCoinSupply")]
        public string TotalCoinSupply { get; set; }

        [JsonProperty("DifficultyAdjustment")]
        public string DifficultyAdjustment { get; set; }

        [JsonProperty("BlockRewardReduction")]
        public string BlockRewardReduction { get; set; }

        [JsonProperty("Algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty("ProofType")]
        public string ProofType { get; set; }

        [JsonProperty("StartDate")]
        public string StartDate { get; set; }

        [JsonProperty("Twitter")]
        public string Twitter { get; set; }

        [JsonProperty("WebsiteUrl")]
        public string WebsiteUrl { get; set; }

        [JsonProperty("Website")]
        public string Website { get; set; }

        [JsonProperty("Sponsor")]
        public Sponsor Sponsor { get; set; }

        [JsonProperty("IndividualSponsor")]
        public IndividualSponsor IndividualSponsor { get; set; }

        [JsonProperty("LastBlockExplorerUpdateTS")]
        public long LastBlockExplorerUpdateTs { get; set; }

        [JsonProperty("BlockNumber")]
        public long BlockNumber { get; set; }

        [JsonProperty("BlockTime")]
        public long BlockTime { get; set; }

        [JsonProperty("NetHashesPerSecond")]
        public double NetHashesPerSecond { get; set; }

        [JsonProperty("TotalCoinsMined")]
        public long TotalCoinsMined { get; set; }

        [JsonProperty("PreviousTotalCoinsMined")]
        public long PreviousTotalCoinsMined { get; set; }

        [JsonProperty("BlockReward")]
        public double BlockReward { get; set; }
    }

    public partial class IndividualSponsor
    {
        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("Link")]
        public string Link { get; set; }

        [JsonProperty("ExcludedCountries")]
        public string ExcludedCountries { get; set; }
    }

    public partial class Sponsor
    {
        [JsonProperty("TextTop")]
        public string TextTop { get; set; }

        [JsonProperty("Link")]
        public string Link { get; set; }

        [JsonProperty("ImageUrl")]
        public string ImageUrl { get; set; }
    }

    public partial class Ico
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("WhitePaper")]
        public string WhitePaper { get; set; }
    }

    public partial class Seo
    {
        [JsonProperty("PageTitle")]
        public string PageTitle { get; set; }

        [JsonProperty("PageDescription")]
        public string PageDescription { get; set; }

        [JsonProperty("BaseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("BaseImageUrl")]
        public string BaseImageUrl { get; set; }

        [JsonProperty("OgImageUrl")]
        public string OgImageUrl { get; set; }

        [JsonProperty("OgImageWidth")]
        public string OgImageWidth { get; set; }

        [JsonProperty("OgImageHeight")]
        public string OgImageHeight { get; set; }
    }

    public partial class CoinFullInfo
    {
        public CoinFullInfo FromJson(string json) => JsonConvert.DeserializeObject<CoinFullInfo>(json, Converter.Settings);
        public string ToJson() => JsonConvert.SerializeObject(this, Converter.Settings);
    }

    public static class CoinFullInfoSerialize
    {
        public static string ToJson(this CoinFullInfo self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    #endregion

    #region CryptoCompare Api Implementation

    public class Constants
    {
        public static string CoinListUrl = "https://min-api.cryptocompare.com/data/all/coinlist";
        public static string CoinFullInfoUrl = "https://www.cryptocompare.com/api/data/coinsnapshotfullbyid/?id=[0]";
    }

    public class Api
    {
        protected RequestDictionaryMessage<CoinList, CoinListData, Dictionary<string, CoinListData>> _CoinList;
        protected RequestMessage<CoinFullInfo, CoinFullInfoData, CoinFullInfoData> _CoinFullInfo;
        public Dictionary<string, CoinListData> CoinList { get { return _CoinList.Data; } }
        public CoinFullInfoData CoinFullInfo { get { return _CoinFullInfo.Data; } }

        public Api()
        {
            Initialize();
        }

        public bool Initialize()
        {
            _CoinList = new CoinListMessage(Constants.CoinListUrl, null);

            return true;
        }

        public bool Refresh()
        {
            _CoinList.Initialize();

            PostRefresh();

            return true;
        }

        public void PostRefresh()
        {

        }

        public CoinFullInfoData GetFullInfo(int id)
        {
            return GetFullInfo(id.ToString());
        }

        public CoinFullInfoData GetFullInfo(string id)
        {
            string[] args = { id };
            _CoinFullInfo = new CoinFullInfoMessage(Constants.CoinFullInfoUrl, args);
            _CoinFullInfo.Initialize();
            return _CoinFullInfo.Data;
        }
    }

    public class CoinListMessage : RequestDictionaryMessage<CoinList, CoinListData, Dictionary<string, CoinListData>>
    {
        public CoinListMessage(string url, string[] args) : base(url, args)
        {
            _replaceHeaders = true;
            _replaceFrom = "\"Response\":\"Success\"";
            _replaceTo = "\"Success\":true";
        }

        public override void Process(IDictionaryResponse<CoinList, CoinListData> mr)
        {
            this._Data = mr.Data;
        }
    }


    public class CoinFullInfoMessage : RequestMessage<CoinFullInfo, CoinFullInfoData, CoinFullInfoData>
    {
        public CoinFullInfoMessage(string url, string[] args) : base(url, args)
        {
            _replaceHeaders = true;
            _replaceFrom = "\"Response\":\"Success\"";
            _replaceTo = "\"Success\":true";
        }

        public override void Process(IResponse<CoinFullInfo, CoinFullInfoData> mr)
        {
            this._Data = mr.Data;
        }
    }

    #endregion
}
