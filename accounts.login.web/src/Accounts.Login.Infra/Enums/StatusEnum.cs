using System.Text.Json.Serialization;

namespace Accounts.Login.Infra.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusEnum
    {
        Inactive = 0,
        Active = 1,
    }
}