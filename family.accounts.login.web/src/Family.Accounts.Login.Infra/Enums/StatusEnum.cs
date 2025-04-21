using System.Text.Json.Serialization;

namespace Family.Accounts.Login.Infra.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusEnum
    {
        Inactive = 0,
        Active = 1,
    }
}