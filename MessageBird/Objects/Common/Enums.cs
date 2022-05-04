using System.Runtime.Serialization;

namespace MessageBird.Objects.Common
{
    public enum Language
    {
        [EnumMember(Value = "nl-nl")]
        Dutch,
        [EnumMember(Value = "de-de")]
        German,
        [EnumMember(Value = "en-gb")]
        English,
        [EnumMember(Value = "en-us")]
        AmericanEnglish,
        [EnumMember(Value = "en-au")]
        AustralianEnglish,
        [EnumMember(Value = "fr-fr")]
        French,
        [EnumMember(Value = "fr-ca")]
        CanadianFrench,
        [EnumMember(Value = "es-es")]
        Spanish,
        [EnumMember(Value = "es-mx")]
        MexicanSpanish,
        [EnumMember(Value = "es-us")]
        AmericanSpanish,
        [EnumMember(Value = "ru-ru")]
        Russian,
        [EnumMember(Value = "zh-cn")]
        Chinese,
        [EnumMember(Value = "is-is")]
        Icelandic,
        [EnumMember(Value = "it-it")]
        Italian,
        [EnumMember(Value = "ja-jp")]
        Japanese,
        [EnumMember(Value = "ko-kr")]
        Korean,
        [EnumMember(Value = "pl-pl")]
        Polish,
        [EnumMember(Value = "pt-br")]
        BrazilianPortuguese,
        [EnumMember(Value = "ro-ro")]
        Romanian,
        [EnumMember(Value = "da-dk")]
        Danish,
        [EnumMember(Value = "en-in")]
        IndianEnglish,
        [EnumMember(Value = "cy-gb")]
        Welsh,
        [EnumMember(Value = "nb-no")]
        Norwegian,
        [EnumMember(Value = "pt-pt")]
        Portuguese,
        [EnumMember(Value = "sv-se")]
        Swedish,
        [EnumMember(Value = "tr-tr")]
        Turkish,
        [EnumMember(Value = "el-gr")]
        Greek,
        [EnumMember(Value = "zh-hk")]
        HongKongChinese,
        [EnumMember(Value = "id-id")]
        Indonesian,
        [EnumMember(Value = "vi-vn")]
        Vietnamese,
        [EnumMember(Value = "th-th")]
        Thai,
        [EnumMember(Value = "ta-in")]
        TamilIndian,
        [EnumMember(Value = "ms-my")]
        Malay
    }

    public enum Voice
    {
        [EnumMember(Value = "male")]
        Male,
        [EnumMember(Value = "female")]
        Female,
    }
}
