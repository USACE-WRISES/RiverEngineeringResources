using System.Text.Json.Serialization;

namespace RiverEngineeringResources.Shared
{
    public class MyStreamModelFunction
    {
        [JsonPropertyName("Functional Category")]
        public string FunctionalCategory { get; set; }

        [JsonPropertyName("Functional Variable")]
        public string FunctionalVariable { get; set; }

        [JsonPropertyName("Functional Statement")]
        public string FunctionalStatement { get; set; }

        [JsonPropertyName("Physical")]
        public string Physical { get; set; }

        [JsonPropertyName("Chemical")]
        public string Chemical { get; set; }

        [JsonPropertyName("Biological")]
        public string Biological { get; set; }

    }
}
