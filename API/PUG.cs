using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChemStoreWebApp.PUG
{
	public static class REST
	{
		private static readonly HttpClient client = new() { BaseAddress=new Uri("https://pubchem.ncbi.nlm.nih.gov/rest/pug/") };

        public static async Task<int[]> GetCIDListAsync(string searchParam)
        {
            string src = $"compound/name/{searchParam}/cids/txt";

            string response = await client.GetStringAsync(src);
            string[] strs = response.Split('\n');
            int[] ints = new int[strs.Length - 1]; // -1 because returned data has an extra line
            for (int i = 0; i < ints.Length; i++)
            {
                ints[i] = int.Parse(strs[i]);
            }

            return ints;
        }
    }

    public static class View
    {
        private static readonly HttpClient client = new() { BaseAddress = new Uri("https://pubchem.ncbi.nlm.nih.gov/rest/pug_view/") };

       /* public record class Chemical(
            [property: JsonPropertyName("RecordNumber")]int CID,
            [property: ]List<string> aliases);*/

        public static async Task<List<int>> GetChemical(int CID)
        {
            string src = $"data/compound/{CID}/JSON";

            string response = await client.GetStringAsync(src);
            // Console.WriteLine(response);

            JsonNode node = JsonNode.Parse(response);
            Console.WriteLine($"Name: {node["Record"]["RecordTitle"]}\n");
            GetSection(node["Record"]["Section"], "Names and Identifiers");
            //await using Stream stream = await client.GetStreamAsync(src);
            //var response = await JsonSerializer.DeserializeAsync<Dictionary<string, Dictionary<string, List<int>>>>(stream);
            // Console.WriteLine(response["IdentifierList"]["CID"].Count);
            //return response["IdentifierList"]["CID"];

            return null;
        }

        private static JsonNode GetSection(JsonNode node, string sectionHeading)
        {
            JsonArray arr = node.AsArray();
            for (int i = 0; i < arr.Count; i++)
            {
                Console.WriteLine($"{i}: {arr[i]["TOCHeading"]}");
                //Console.WriteLine($"{arr[i]["TOCHeading"]}");
                if (arr[i]["TOCHeading"].Deserialize<string>() == sectionHeading) return arr[i];
            }

            return null;
        }
    }

}