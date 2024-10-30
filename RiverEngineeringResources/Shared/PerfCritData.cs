using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using MudBlazor;
using System.Linq;
using System.Threading.Tasks;

namespace RiverEngineeringResources.Shared
{
    public class PerfCritData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SourceType { get; set; }
        public string SourceName { get; set; }
        public string DataType { get; set; } // "Quantitative" or "Semi-Quantitative"
        public string XName { get; set; }
        public string YName { get; set; }
        public string Values { get; set; } // Raw string values to parse later

        public bool CanPlot => ParsedValues.Count >= 2;

        // Chart-related properties
        public double SelectedX { get; set; }
        public double CalculatedY { get; set; }
        public double OverrideY { get; set; } // Property to store overridden Y value
        public string[] XAxisLabels => ParsedValues.Select(item => item.Label ?? item.XValue.ToString()).ToArray();
        public List<ChartSeries> ChartData => new()
        {
            new ChartSeries
            {
                Name = "Performance Data",
                Data = ParsedValues.Select(item => item.YValue).ToArray()
            }
        };

        // Make ParsedValues a modifiable list
        private List<DataPoint> _parsedValues;
        public List<DataPoint> ParsedValues
        {
            get
            {
                if (_parsedValues == null)
                {
                    _parsedValues = ParseValues(Values, DataType);
                }
                return _parsedValues;
            }
            set
            {
                _parsedValues = value;
                UpdateValuesString();
            }
        }

        private List<DataPoint> ParseValues(string values, string dataType)
        {
            var dataPoints = new List<DataPoint>();
            var entries = values.Split(' ');

            foreach (var entry in entries)
            {
                var parts = entry.Split(',');
                if (dataType == "Quantitative" && parts.Length == 2)
                {
                    if (double.TryParse(parts[0], out double xValue) && double.TryParse(parts[1], out double yValue))
                    {
                        dataPoints.Add(new DataPoint(xValue, yValue));
                    }
                }
                else if (dataType == "Semi-Quantitative" && parts.Length == 2)
                {
                    var label = parts[0].Trim('\'');
                    if (double.TryParse(parts[1], out double yValue))
                    {
                        dataPoints.Add(new DataPoint(label, yValue));
                    }
                }
            }

            return dataPoints;
        }

        private void UpdateValuesString()
        {
            Values = string.Join(" ", ParsedValues.Select(p =>
                DataType == "Quantitative" ? $"{p.XValue},{p.YValue}" : $"'{p.Label}',{p.YValue}"));
        }
    }

    public class DataPoint
    {
        public string Label { get; set; }
        public double XValue { get; set; }
        public double YValue { get; set; }

        public DataPoint(double xValue, double yValue)
        {
            XValue = xValue;
            YValue = yValue;
        }

        public DataPoint(string label, double yValue)
        {
            Label = label;
            YValue = yValue;
        }
    }

    public class CsvRow
    {
        public PerfCritData PCS { get; set; } = new PerfCritData();
    }

    public class CsvRowMap : ClassMap<CsvRow>
    {
        public CsvRowMap()
        {
            Map(m => m.PCS.Name).Name("PCS Name");
            Map(m => m.PCS.Description).Name("PCS Description");
            Map(m => m.PCS.SourceType).Name("PCS Source Type");
            Map(m => m.PCS.SourceName).Name("PCS Source Name");
            Map(m => m.PCS.DataType).Name("PCS Data Type");
            Map(m => m.PCS.XName).Name("PCS X Data Name");
            Map(m => m.PCS.YName).Name("PCS Y Data Name");
            Map(m => m.PCS.Values).Name("PCS Values");
        }
    }

    public class PerformanceCriteriaService
    {
        public PerfCritData ConvertCsvRowToPerformanceCriteriaData(CsvRow row)
        {
            return new PerfCritData
            {
                Name = row.PCS.Name,
                Description = row.PCS.Description,
                SourceType = row.PCS.SourceType,
                SourceName = row.PCS.SourceName,
                DataType = row.PCS.DataType,
                XName = row.PCS.XName,
                YName = row.PCS.YName,
                Values = row.PCS.Values
            };
        }

        public async Task<List<PerfCritData>> ReadCsvAsync(string csvContent)
        {
            var criteriaDataList = new List<PerfCritData>();

            using var reader = new StringReader(csvContent);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvRowMap>(); // Ensure CsvRowMap is defined

            await foreach (var row in csv.GetRecordsAsync<CsvRow>())
            {
                var criteriaData = ConvertCsvRowToPerformanceCriteriaData(row);
                criteriaDataList.Add(criteriaData);
            }

            return criteriaDataList;
        }

        public void WriteCsv(string filePath, List<CsvRow> rows)
        {
            using var writer = new StreamWriter(filePath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CsvRowMap>();
            csv.WriteRecords(rows);
        }
    }
}