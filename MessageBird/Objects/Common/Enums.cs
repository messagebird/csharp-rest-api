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
        BrazilianPortugese,
        [EnumMember(Value = "ro-ro")]
        Romanian
    }

    public enum Voice
    {
        [EnumMember(Value = "male")]
        Male,
        [EnumMember(Value = "female")]
        Female,
    }
}
