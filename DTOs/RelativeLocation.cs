using System.Text.Json.Serialization;

namespace FireReporter.DTOs;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RelativeLocation
{
    Unknown,
    Inside,
    Outside
}