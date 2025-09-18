using System.Text.Json.Serialization;

namespace Team.Accounts.Login.Infra.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusEnum
    {
        Inactive = 0,
        Active = 1,
    }
}