using System.ComponentModel;
using System.Runtime.Serialization;

namespace YazilimAcademyPayments.WebApi.Domain.Enums;

/// <summary>
/// Enumeration of ISO 4217 currency codes, indexed with their respective ISO 4217 numeric currency codes. 
/// Only codes support in .Net with RegionInfo objects are listed
/// </summary>
public enum CurrencyCode
{
    [EnumMember(Value = "AED")]
    [Description("United Arab Emirates dirham")]
    AED = 784,

    [EnumMember(Value = "AFN")]
    [Description("Afghan afghani")]
    AFN = 971,

    [EnumMember(Value = "ALL")]
    [Description("Albanian lek")]
    ALL = 8,

    [EnumMember(Value = "AMD")]
    [Description("Armenian dram")]
    AMD = 51,

    [EnumMember(Value = "ANG")]
    [Description("Netherlands Antillean guilder")]
    ANG = 532,

    [EnumMember(Value = "AOA")]
    [Description("Angolan kwanza")]
    AOA = 973,

    [EnumMember(Value = "ARS")]
    [Description("Argentine peso")]
    ARS = 32,

    [EnumMember(Value = "AUD")]
    [Description("Australian dollar")]
    AUD = 36,

    [EnumMember(Value = "AWG")]
    [Description("Aruban florin")]
    AWG = 533,

    [EnumMember(Value = "AZN")]
    [Description("Azerbaijani manat")]
    AZN = 944,

    [EnumMember(Value = "BAM")]
    [Description("Bosnia and Herzegovina convertible mark")]
    BAM = 977,

    [EnumMember(Value = "BBD")]
    [Description("Barbados dollar")]
    BBD = 52,

    [EnumMember(Value = "BDT")]
    [Description("Bangladeshi taka")]
    BDT = 50,

    [EnumMember(Value = "BGN")]
    [Description("Bulgarian lev")]
    BGN = 975,

    [EnumMember(Value = "BHD")]
    [Description("Bahraini dinar")]
    BHD = 48,

    [EnumMember(Value = "BIF")]
    [Description("Burundian franc")]
    BIF = 108,

    [EnumMember(Value = "BMD")]
    [Description("Bermudian dollar")]
    BMD = 60,

    [EnumMember(Value = "BND")]
    [Description("Brunei dollar")]
    BND = 96,

    [EnumMember(Value = "BOB")]
    [Description("Boliviano")]
    BOB = 68,

    [EnumMember(Value = "BOV")]
    [Description("Bolivian Mvdol (funds code)")]
    BOV = 984,

    [EnumMember(Value = "BRL")]
    [Description("Brazilian real")]
    BRL = 986,

    [EnumMember(Value = "BSD")]
    [Description("Bahamian dollar")]
    BSD = 44,

    [EnumMember(Value = "BTN")]
    [Description("Bhutanese ngultrum")]
    BTN = 64,

    [EnumMember(Value = "BWP")]
    [Description("Botswana pula")]
    BWP = 72,

    [EnumMember(Value = "BYN")]
    [Description("Belarusian ruble")]
    BYN = 933,

    [EnumMember(Value = "BZD")]
    [Description("Belize dollar")]
    BZD = 84,

    [EnumMember(Value = "CAD")]
    [Description("Canadian dollar")]
    CAD = 124,

    [EnumMember(Value = "CDF")]
    [Description("Congolese franc")]
    CDF = 976,

    [EnumMember(Value = "CHE")]
    [Description("WIR euro (complementary currency)")]
    CHE = 947,

    [EnumMember(Value = "CHF")]
    [Description("Swiss franc")]
    CHF = 756,

    [EnumMember(Value = "CHW")]
    [Description("WIR franc (complementary currency)")]
    CHW = 948,

    [EnumMember(Value = "CLF")]
    [Description("Unidad de Fomento (funds code)")]
    CLF = 990,

    [EnumMember(Value = "CLP")]
    [Description("Chilean peso")]
    CLP = 152,

    [EnumMember(Value = "COP")]
    [Description("Colombian peso")]
    COP = 170,

    [EnumMember(Value = "COU")]
    [Description("Unidad de Valor Real (UVR) (funds code)")]
    COU = 970,

    [EnumMember(Value = "CRC")]
    [Description("Costa Rican colon")]
    CRC = 188,

    [EnumMember(Value = "CUC")]
    [Description("Cuban convertible peso")]
    CUC = 931,

    [EnumMember(Value = "CUP")]
    [Description("Cuban peso")]
    CUP = 192,

    [EnumMember(Value = "CVE")]
    [Description("Cape Verdean escudo")]
    CVE = 132,

    [EnumMember(Value = "CZK")]
    [Description("Czech koruna")]
    CZK = 203,

    [EnumMember(Value = "DJF")]
    [Description("Djiboutian franc")]
    DJF = 262,

    [EnumMember(Value = "DKK")]
    [Description("Danish krone")]
    DKK = 208,

    [EnumMember(Value = "DOP")]
    [Description("Dominican peso")]
    DOP = 214,

    [EnumMember(Value = "DZD")]
    [Description("Algerian dinar")]
    DZD = 12,

    [EnumMember(Value = "EGP")]
    [Description("Egyptian pound")]
    EGP = 818,

    [EnumMember(Value = "ERN")]
    [Description("Eritrean nakfa")]
    ERN = 232,

    [EnumMember(Value = "ETB")]
    [Description("Ethiopian birr")]
    ETB = 230,

    [EnumMember(Value = "EUR")]
    [Description("Euro")]
    EUR = 978,

    [EnumMember(Value = "FJD")]
    [Description("Fiji dollar")]
    FJD = 242,

    [EnumMember(Value = "FKP")]
    [Description("Falkland Islands pound")]
    FKP = 238,

    [EnumMember(Value = "GBP")]
    [Description("Pound sterling")]
    GBP = 826,

    [EnumMember(Value = "GEL")]
    [Description("Georgian lari")]
    GEL = 981,

    [EnumMember(Value = "GHS")]
    [Description("Ghanaian cedi")]
    GHS = 936,

    [EnumMember(Value = "GIP")]
    [Description("Gibraltar pound")]
    GIP = 292,

    [EnumMember(Value = "GMD")]
    [Description("Gambian dalasi")]
    GMD = 270,

    [EnumMember(Value = "GNF")]
    [Description("Guinean franc")]
    GNF = 324,

    [EnumMember(Value = "GTQ")]
    [Description("Guatemalan quetzal")]
    GTQ = 320,

    [EnumMember(Value = "GYD")]
    [Description("Guyanese dollar")]
    GYD = 328,

    [EnumMember(Value = "HKD")]
    [Description("Hong Kong dollar")]
    HKD = 344,

    [EnumMember(Value = "HNL")]
    [Description("Honduran lempira")]
    HNL = 340,

    [EnumMember(Value = "HRK")]
    [Description("Croatian kuna")]
    HRK = 191,

    [EnumMember(Value = "HTG")]
    [Description("Haitian gourde")]
    HTG = 332,

    [EnumMember(Value = "HUF")]
    [Description("Hungarian forint")]
    HUF = 348,

    [EnumMember(Value = "IDR")]
    [Description("Indonesian rupiah")]
    IDR = 360,

    [EnumMember(Value = "ILS")]
    [Description("Israeli new shekel")]
    ILS = 376,

    [EnumMember(Value = "INR")]
    [Description("Indian rupee")]
    INR = 356,

    [EnumMember(Value = "IQD")]
    [Description("Iraqi dinar")]
    IQD = 368,

    [EnumMember(Value = "IRR")]
    [Description("Iranian rial")]
    IRR = 364,

    [EnumMember(Value = "ISK")]
    [Description("Icelandic króna (plural: krónur)")]
    ISK = 352,

    [EnumMember(Value = "JMD")]
    [Description("Jamaican dollar")]
    JMD = 388,

    [EnumMember(Value = "JOD")]
    [Description("Jordanian dinar")]
    JOD = 400,

    [EnumMember(Value = "JPY")]
    [Description("Japanese yen")]
    JPY = 392,

    [EnumMember(Value = "KES")]
    [Description("Kenyan shilling")]
    KES = 404,

    [EnumMember(Value = "KGS")]
    [Description("Kyrgyzstani som")]
    KGS = 417,

    [EnumMember(Value = "KHR")]
    [Description("Cambodian riel")]
    KHR = 116,

    [EnumMember(Value = "KMF")]
    [Description("Comoro franc")]
    KMF = 174,

    [EnumMember(Value = "KPW")]
    [Description("North Korean won")]
    KPW = 408,

    [EnumMember(Value = "KRW")]
    [Description("South Korean won")]
    KRW = 410,

    [EnumMember(Value = "KWD")]
    [Description("Kuwaiti dinar")]
    KWD = 414,

    [EnumMember(Value = "KYD")]
    [Description("Cayman Islands dollar")]
    KYD = 136,

    [EnumMember(Value = "KZT")]
    [Description("Kazakhstani tenge")]
    KZT = 398,

    [EnumMember(Value = "LAK")]
    [Description("Lao kip")]
    LAK = 418,

    [EnumMember(Value = "LBP")]
    [Description("Lebanese pound")]
    LBP = 422,

    [EnumMember(Value = "LKR")]
    [Description("Sri Lankan rupee")]
    LKR = 144,

    [EnumMember(Value = "LRD")]
    [Description("Liberian dollar")]
    LRD = 430,

    [EnumMember(Value = "LSL")]
    [Description("Lesotho loti")]
    LSL = 426,

    [EnumMember(Value = "LYD")]
    [Description("Libyan dinar")]
    LYD = 434,

    [EnumMember(Value = "MAD")]
    [Description("Moroccan dirham")]
    MAD = 504,

    [EnumMember(Value = "MDL")]
    [Description("Moldovan leu")]
    MDL = 498,

    [EnumMember(Value = "MGA")]
    [Description("Malagasy ariary")]
    MGA = 969,

    [EnumMember(Value = "MKD")]
    [Description("Macedonian denar")]
    MKD = 807,

    [EnumMember(Value = "MMK")]
    [Description("Myanmar kyat")]
    MMK = 104,

    [EnumMember(Value = "MNT")]
    [Description("Mongolian tögrög")]
    MNT = 496,

    [EnumMember(Value = "MOP")]
    [Description("Macanese pataca")]
    MOP = 446,

    [EnumMember(Value = "MRU")]
    [Description("Mauritanian ouguiya")]
    MRU = 929,

    [EnumMember(Value = "MUR")]
    [Description("Mauritian rupee")]
    MUR = 480,

    [EnumMember(Value = "MVR")]
    [Description("Maldivian rufiyaa")]
    MVR = 462,

    [EnumMember(Value = "MWK")]
    [Description("Malawian kwacha")]
    MWK = 454,

    [EnumMember(Value = "MXN")]
    [Description("Mexican peso")]
    MXN = 484,

    [EnumMember(Value = "MXV")]
    [Description("Mexican Unidad de Inversion (UDI) (funds code)")]
    MXV = 979,

    [EnumMember(Value = "MYR")]
    [Description("Malaysian ringgit")]
    MYR = 458,

    [EnumMember(Value = "MZN")]
    [Description("Mozambican metical")]
    MZN = 943,

    [EnumMember(Value = "NAD")]
    [Description("Namibian dollar")]
    NAD = 516,

    [EnumMember(Value = "NGN")]
    [Description("Nigerian naira")]
    NGN = 566,

    [EnumMember(Value = "NIO")]
    [Description("Nicaraguan córdoba")]
    NIO = 558,

    [EnumMember(Value = "NOK")]
    [Description("Norwegian krone")]
    NOK = 578,

    [EnumMember(Value = "NPR")]
    [Description("Nepalese rupee")]
    NPR = 524,

    [EnumMember(Value = "NZD")]
    [Description("New Zealand dollar")]
    NZD = 554,

    [EnumMember(Value = "OMR")]
    [Description("Omani rial")]
    OMR = 512,

    [EnumMember(Value = "PAB")]
    [Description("Panamanian balboa")]
    PAB = 590,

    [EnumMember(Value = "PEN")]
    [Description("Peruvian sol")]
    PEN = 604,

    [EnumMember(Value = "PGK")]
    [Description("Papua New Guinean kina")]
    PGK = 598,

    [EnumMember(Value = "PHP")]
    [Description("Philippine peso")]
    PHP = 608,

    [EnumMember(Value = "PKR")]
    [Description("Pakistani rupee")]
    PKR = 586,

    [EnumMember(Value = "PLN")]
    [Description("Polish złoty")]
    PLN = 985,

    [EnumMember(Value = "PYG")]
    [Description("Paraguayan guaraní")]
    PYG = 600,

    [EnumMember(Value = "QAR")]
    [Description("Qatari riyal")]
    QAR = 634,

    [EnumMember(Value = "RON")]
    [Description("Romanian leu")]
    RON = 946,

    [EnumMember(Value = "RSD")]
    [Description("Serbian dinar")]
    RSD = 941,

    [EnumMember(Value = "CNY")]
    [Description("Renminbi[14]")]
    CNY = 156,

    [EnumMember(Value = "RUB")]
    [Description("Russian ruble")]
    RUB = 643,

    [EnumMember(Value = "RWF")]
    [Description("Rwandan franc")]
    RWF = 646,

    [EnumMember(Value = "SAR")]
    [Description("Saudi riyal")]
    SAR = 682,

    [EnumMember(Value = "SBD")]
    [Description("Solomon Islands dollar")]
    SBD = 90,

    [EnumMember(Value = "SCR")]
    [Description("Seychelles rupee")]
    SCR = 690,

    [EnumMember(Value = "SDG")]
    [Description("Sudanese pound")]
    SDG = 938,

    [EnumMember(Value = "SEK")]
    [Description("Swedish krona (plural: kronor)")]
    SEK = 752,

    [EnumMember(Value = "SGD")]
    [Description("Singapore dollar")]
    SGD = 702,

    [EnumMember(Value = "SHP")]
    [Description("Saint Helena pound")]
    SHP = 654,

    [EnumMember(Value = "SLL")]
    [Description("Sierra Leonean leone")]
    SLL = 694,

    [EnumMember(Value = "SLE")]
    [Description("Sierra Leonean leone")]
    SLE = 925,

    [EnumMember(Value = "SOS")]
    [Description("Somali shilling")]
    SOS = 706,

    [EnumMember(Value = "SRD")]
    [Description("Surinamese dollar")]
    SRD = 968,

    [EnumMember(Value = "SSP")]
    [Description("South Sudanese pound")]
    SSP = 728,

    [EnumMember(Value = "STN")]
    [Description("São Tomé and Príncipe dobra")]
    STN = 930,

    [EnumMember(Value = "SVC")]
    [Description("Salvadoran colón")]
    SVC = 222,

    [EnumMember(Value = "SYP")]
    [Description("Syrian pound")]
    SYP = 760,

    [EnumMember(Value = "SZL")]
    [Description("Swazi lilangeni")]
    SZL = 748,

    [EnumMember(Value = "THB")]
    [Description("Thai baht")]
    THB = 764,

    [EnumMember(Value = "TJS")]
    [Description("Tajikistani somoni")]
    TJS = 972,

    [EnumMember(Value = "TMT")]
    [Description("Turkmenistan manat")]
    TMT = 934,

    [EnumMember(Value = "TND")]
    [Description("Tunisian dinar")]
    TND = 788,

    [EnumMember(Value = "TOP")]
    [Description("Tongan paʻanga")]
    TOP = 776,

    [EnumMember(Value = "TRY")]
    [Description("Turkish lira")]
    TRY = 949,

    [EnumMember(Value = "TTD")]
    [Description("Trinidad and Tobago dollar")]
    TTD = 780,

    [EnumMember(Value = "TWD")]
    [Description("New Taiwan dollar")]
    TWD = 901,

    [EnumMember(Value = "TZS")]
    [Description("Tanzanian shilling")]
    TZS = 834,

    [EnumMember(Value = "UAH")]
    [Description("Ukrainian hryvnia")]
    UAH = 980,

    [EnumMember(Value = "UGX")]
    [Description("Ugandan shilling")]
    UGX = 800,

    [EnumMember(Value = "USD")]
    [Description("United States dollar")]
    USD = 840,

    [EnumMember(Value = "USN")]
    [Description("United States dollar (next day) (funds code)")]
    USN = 997,

    [EnumMember(Value = "UYI")]
    [Description("Uruguay Peso en Unidades Indexadas (URUIURUI) (funds code)")]
    UYI = 940,

    [EnumMember(Value = "UYU")]
    [Description("Uruguayan peso")]
    UYU = 858,

    [EnumMember(Value = "UYW")]
    [Description("Unidad previsional")]
    UYW = 927,

    [EnumMember(Value = "UZS")]
    [Description("Uzbekistan som")]
    UZS = 860,

    [EnumMember(Value = "VED")]
    [Description("Venezuelan bolívar digital")]
    VED = 926,

    [EnumMember(Value = "VES")]
    [Description("Venezuelan bolívar soberano")]
    VES = 928,

    [EnumMember(Value = "VND")]
    [Description("Vietnamese đồng")]
    VND = 704,

    [EnumMember(Value = "VUV")]
    [Description("Vanuatu vatu")]
    VUV = 548,

    [EnumMember(Value = "WST")]
    [Description("Samoan tala")]
    WST = 882,

    [EnumMember(Value = "XAF")]
    [Description("CFA franc BEAC")]
    XAF = 950,

    [EnumMember(Value = "XAG")]
    [Description("Silver (one troy ounce)")]
    XAG = 961,

    [EnumMember(Value = "XAU")]
    [Description("Gold (one troy ounce)")]
    XAU = 959,

    [EnumMember(Value = "XBA")]
    [Description("European Composite Unit (EURCO) (bond market unit)")]
    XBA = 955,

    [EnumMember(Value = "XBB")]
    [Description("European Monetary Unit (E.M.U.-6) (bond market unit)")]
    XBB = 956,

    [EnumMember(Value = "XBC")]
    [Description("European Unit of Account 9 (E.U.A.-9) (bond market unit)")]
    XBC = 957,

    [EnumMember(Value = "XBD")]
    [Description("European Unit of Account 17 (E.U.A.-17) (bond market unit)")]
    XBD = 958,

    [EnumMember(Value = "XCD")]
    [Description("East Caribbean dollar")]
    XCD = 951,

    [EnumMember(Value = "XDR")]
    [Description("Special drawing rights")]
    XDR = 960,

    [EnumMember(Value = "XOF")]
    [Description("CFA franc BCEAO")]
    XOF = 952,

    [EnumMember(Value = "XPD")]
    [Description("Palladium (one troy ounce)")]
    XPD = 964,

    [EnumMember(Value = "XPF")]
    [Description("CFP franc (franc Pacifique)")]
    XPF = 953,

    [EnumMember(Value = "XPT")]
    [Description("Platinum (one troy ounce)")]
    XPT = 962,

    [EnumMember(Value = "XSU")]
    [Description("SUCRE")]
    XSU = 994,

    [EnumMember(Value = "XTS")]
    [Description("Code reserved for testing")]
    XTS = 963,

    [EnumMember(Value = "XUA")]
    [Description("ADB Unit of Account")]
    XUA = 965,

    [EnumMember(Value = "XXX")]
    [Description("No currency")]
    XXX = 999,

    [EnumMember(Value = "YER")]
    [Description("Yemeni rial")]
    YER = 886,

    [EnumMember(Value = "ZAR")]
    [Description("South African rand")]
    ZAR = 710,

    [EnumMember(Value = "ZMW")]
    [Description("Zambian kwacha")]
    ZMW = 967,

    [EnumMember(Value = "ZWL")]
    [Description("Zimbabwean dollar")]
    ZWL = 932
}