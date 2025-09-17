using System.Text.Json.Serialization;

namespace GamePicker.Core.Models
{
    //-----A OPCAO -- QUE APARECE NO SWAGGER VEM POR CONTA DO ENUM, NÃO SENDO POSSIVEL REMOVER SE DEIXAR DE SER ENUM-----//
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Platform
    {
        PC,
        Browser,
        Both
    }
}