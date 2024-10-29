using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using RiverEngineeringResources.Shared;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;

namespace RiverEngineeringResources.Shared
{
    public class SharedFunctions
    {

        public static IEnumerable<string> CsvToJson(string fileName, char delim = '|')
        {
            var lines = File.ReadLines(fileName);
            var hdr = new List<string>(lines.First().Trim().Split(delim));
            foreach (var l in lines.Skip(1).Where(l => (l.Trim() != String.Empty)))
            {
                var val = l.Trim().Split(delim);
                var ds = hdr.Zip(val, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
                yield return JsonSerializer.Serialize(ds);
            }
        }

        public static IEnumerable<string> CsvToJsonFromString(string csvContent, char delim = ',')
        {
            var lines = csvContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var hdr = ParseCsvLine(lines.First(), delim);

            foreach (var line in lines.Skip(1).Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                var fields = ParseCsvLine(line, delim);
                var ds = hdr.Zip(fields, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
                yield return JsonSerializer.Serialize(ds);
            }
        }

        public static IEnumerable<string> CsvToJsonFromString2(string csvContent, char delim = ',')
        {
            using (var reader = new StringReader(csvContent))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = delim.ToString(),
                BadDataFound = null // Suppress bad data warnings if needed
            }))
            {
                var records = csv.GetRecords<dynamic>();
                foreach (var record in records)
                {
                    var ds = ((IDictionary<string, object>)record)
                             .ToDictionary(k => k.Key, v => v.Value?.ToString());
                    yield return JsonSerializer.Serialize(ds);
                }
            }
        }

        private static List<string> ParseCsvLine(string line, char delim)
        {
            var pattern = $"(?<=^|{delim})(\"(?:[^\"]|\"\")*\"|[^{delim}]*)";
            var matches = Regex.Matches(line, pattern);

            return matches.Select(m => m.Value.Trim().Trim('"').Replace("\"\"", "\"")).ToList();
        }

    }
}
