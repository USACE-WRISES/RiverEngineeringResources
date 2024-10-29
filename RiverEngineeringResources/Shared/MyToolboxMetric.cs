using System.Text.Json.Serialization;

namespace RiverEngineeringResources.Shared
{
    public class MyToolboxMetric
    {
        // New property that combines Assessment, Year, and MetricName
        public string UniqueID
        {
            get
            {
                return $"{Assessment} - {Year} - {MetricName}";
            }
        }

        [JsonPropertyName("Assessment")]
        public string Assessment { get; set; }

        [JsonPropertyName("Year")]
        public string Year { get; set; }

        [JsonPropertyName("Metric ID")]
        public string MetricShortName { get; set; }

        [JsonPropertyName("Metric Name")]
        public string MetricName { get; set; }

        [JsonPropertyName("Performance Standard")]
        public string PerformanceStandard { get; set; }

        [JsonPropertyName("Performance Standard Source")]
        public string PerformanceStandardSource { get; set; }

        [JsonPropertyName("Method")]
        public string Method { get; set; }

        [JsonPropertyName("Stratification")]
        public string Stratification { get; set; }

        [JsonPropertyName("Source")]
        public string Source { get; set; }

        [JsonPropertyName("Tier")]
        public string Tier { get; set; }

        [JsonPropertyName("Catchment hydrology")]
        public string CatchmentHydrology { get; set; }

        [JsonPropertyName("Surface water storage")]
        public string SurfaceWaterStorage { get; set; }

        [JsonPropertyName("Reach inflow")]
        public string ReachInflow { get; set; }

        [JsonPropertyName("Flow duration")]
        public string FlowDuration { get; set; }

        [JsonPropertyName("Flow alteration")]
        public string FlowAlteration { get; set; }

        [JsonPropertyName("Low flow dynamics")]
        public string LowFlowDynamics { get; set; }

        [JsonPropertyName("Baseflow dynamics")]
        public string BaseflowDynamics { get; set; }

        [JsonPropertyName("High flow dynamics")]
        public string HighFlowDynamics { get; set; }

        [JsonPropertyName("Floodplain connectivity")]
        public string FloodplainConnectivity { get; set; }

        [JsonPropertyName("Hyporheic connectivity")]
        public string HyporheicConnectivity { get; set; }

        [JsonPropertyName("Channel evolution")]
        public string ChannelEvolution { get; set; }

        [JsonPropertyName("Lateral stability")]
        public string LateralStability { get; set; }

        [JsonPropertyName("Planform change")]
        public string PlanformChange { get; set; }

        [JsonPropertyName("Sediment continuity")]
        public string SedimentContinuity { get; set; }

        [JsonPropertyName("Large wood")]
        public string LargeWood { get; set; }

        [JsonPropertyName("Bed composition")]
        public string BedComposition { get; set; }

        [JsonPropertyName("Light and thermal regime")]
        public string LightAndThermalRegime { get; set; }

        [JsonPropertyName("Carbon processing")]
        public string CarbonProcessing { get; set; }

        [JsonPropertyName("Nutrient cycling")]
        public string NutrientCycling { get; set; }

        [JsonPropertyName("Water and soil quality")]
        public string WaterAndSoilQuality { get; set; }

        [JsonPropertyName("Habitat provision")]
        public string HabitatProvision { get; set; }

        [JsonPropertyName("Population support")]
        public string PopulationSupport { get; set; }

        [JsonPropertyName("Community dynamics")]
        public string CommunityDynamics { get; set; }

        [JsonPropertyName("Watershed connectivity")]
        public string WatershedConnectivity { get; set; }


    }
}
