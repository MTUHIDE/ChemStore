using System;
using System.Collections.Generic;
using System.Drawing;
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
            string response = "";

            try
            {
                response = await client.GetStringAsync(src);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return new int[] { -1 };
            }

            string[] strs = response.Split('\n');
            int[] ints = new int[strs.Length - 1]; // -1 because returned data has an extra line

            // Testing if the search term exists in PUG REST's api
            if (strs[0].Contains("Status")) // A status is return when a error occurs (tested on 404 errors)
            {
                ints[0] = -1; // return an error code into the first value of ints
                string[] statusSplit = strs[0].Split(" "); // Should have "Status:" in statusSplit[0], and the code in statusSplit[1]
                ints[1] = int.Parse(statusSplit[1]); // Parse the error code in statusSplit[1]
                return ints;
            }

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
            //Console.WriteLine($"Name: {node["Record"]["RecordTitle"]}\n");
            //GetSection(node["Record"]["Section"], "Names and Identifiers");
            //await using Stream stream = await client.GetStreamAsync(src);
            //var response = await JsonSerializer.DeserializeAsync<Dictionary<string, Dictionary<string, List<int>>>>(stream);
            // Console.WriteLine(response["IdentifierList"]["CID"].Count);
            //return response["IdentifierList"]["CID"];

            // Parse Method Tests

            // CID
            Console.WriteLine("CID: " + GetCID(node));

            // Top 5 Symonoms
            string[] symonoms = getSymonoms(node);
            for (int i = 0;i < symonoms.Length; i++)
                Console.WriteLine("Symonom " + (i+1) + ": " + symonoms[i]);

            // Molecular Weight Value
            Console.WriteLine("Molecular Weight Value: " + getMolecularWeightValue(node).ToString("0.##")); // This will round to 2 decimal places

            // Molecular Weight Unit
            Console.WriteLine("Molecular Weight Unit: " + getMolecularWeightUnit(node));

            // Molecular Weight Combo
            Console.WriteLine("Molecular Weight: " + getMolecularWeight(node));

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

        private static int GetCID(JsonNode obj)
        {
            return obj["Record"]["RecordNumber"].Deserialize<int>();
        }

        private static string getRecordTitle (JsonNode obj)
        {
            return obj["Record"]["RecordTitle"].Deserialize<string>();
        }

        private static int getSectionID(JsonNode obj, string TOCHeading)
        {
            // Declerations
            int i, recordSectionID = -1;

            // Calculate the desired TOCHeading sectionId

            //Console.WriteLine("    Desired Heading: " + TOCHeading);
            //Console.WriteLine("    " + obj["Record"]["Section"].AsArray().Count);
            for (i = 0; i < obj["Record"]["Section"].AsArray().Count; i++) // For the size of Record
            {
                //Console.WriteLine("    Current Section Name: " + obj["Record"]["Section"][i]["TOCHeading"].ToString());
                if (obj["Record"]["Section"][i]["TOCHeading"].ToString().Equals(TOCHeading)) // If the current section is the desired heading, I do not believe I need to repeat this within the sections
                {
                    recordSectionID = i;
                    //Console.WriteLine(nameSectionID);
                    break;
                }
            }

            return recordSectionID;
        }

        private static string[] getSymonoms(JsonNode obj) 
        {
            // Declerations
            int i, size;
            int nameSectionID = getSectionID(obj, "Names and Identifiers"); // Get the "Names and Identifiers" section ID
            string[] topFiveSymonoms = new string[5];

            // Calculates the size of symonom's array
            size = obj["Record"]["Section"][nameSectionID]["Section"][4]["Section"][1]["Information"][0]["Value"]["StringWithMarkup"].AsArray().Count; // Should be 64 for Uranium

            /*
            Console.WriteLine(size);
            */

            string[] allSymonoms = new string[size]; // Need to create the array with a given size, but size changes from compound to compound

            /*
            string testing = obj["Record"]["Section"][nameSectionID]["Section"][4]["Section"][1]["Information"][0]["Value"]["StringWithMarkup"][0]["String"].ToString();
            Console.WriteLine(testing);
            */

            // Gather all Symonoms
            for (i = 0; i < 64; i++)
                allSymonoms[i] = obj["Record"]["Section"][nameSectionID]["Section"][4]["Section"][1]["Information"][0]["Value"]["StringWithMarkup"][i]["String"].ToString();

            // Copy top 5 Symonoms from allSymonoms (should this be from obj, so it could become a thread process?) 
            for (i = 0; i < 5; i++)
                topFiveSymonoms[i] = allSymonoms[i];

            return topFiveSymonoms;
        }

        private static double getMolecularWeightValue(JsonNode obj)
        {
            int chemPropertiesSectionID = getSectionID(obj, "Chemical and Physical Properties");  // Get the "Chemical and Physical Properties" sectionId

            //Console.WriteLine(chemPropertiesSectionID);

            return double.Parse(obj["Record"]["Section"][chemPropertiesSectionID]["Section"][0]["Section"][0]["Information"][0]["Value"]["StringWithMarkup"][0]["String"].ToString());
        }

        private static string getMolecularWeightUnit(JsonNode obj)
        {
            int chemPropertiesSectionID = getSectionID(obj, "Chemical and Physical Properties");  // Get the "Chemical and Physical Properties" sectionId

            return obj["Record"]["Section"][chemPropertiesSectionID]["Section"][0]["Section"][0]["Information"][0]["Value"]["Unit"].ToString();
        }

        private static string getMolecularWeight(JsonNode obj)
        {
            // Declerations & Initializations
            double value = getMolecularWeightValue(obj);
            string unit = getMolecularWeightUnit(obj);

            string combo = "" + value + " " + unit;

            return combo;
        }
    
        private static string getPubChemStorageCondition(JsonNode obj)
        {
            int chemPropertiesSectionID = getSectionID(obj, "Safety and Hazards");  // Get the "Safety and Hazards" sectionId (should be 10)

            return obj["Record"]["Section"][chemPropertiesSectionID]["Section"][5]["Section"][0]["Information"][0]["Value"]["StringWithMarkup"][0]["String"].ToString();
        }

        private static string[] getHCodes(JsonNode obj)
        {
            int chemPropertiesSectionID = getSectionID(obj, "Safety and Hazards");  // Get the "Safety and Hazards" sectionId (should be 10)

            var hCodesDest = obj["Record"]["Section"][chemPropertiesSectionID]["Section"][0]["Section"][0]["Information"][2]["Value"]["StringWithMarkup"]; //declare destination to iterate through
            
            int size = hCodesDest.AsArray().Count; //get size so we can iterate through
            string[] hCodes = new string[size]; //declare string array of size

            for (int i = 0; i < size; i++)
            {
                hCodes[i] = hCodesDest[i]["String"].ToString(); //save each hCode from our json
            }

            return hCodes;
        }

        private static string getPCodes(JsonNode obj)
        {
            int chemPropertiesSectionID = getSectionID(obj, "Safety and Hazards");  // Get the "Safety and Hazards" sectionId (should be 10)

            return obj["Record"]["Section"][chemPropertiesSectionID]["Section"][0]["Section"][0]["Information"][3]["Value"]["StringWithMarkup"][0]["String"].ToString();
        }
    }
}   