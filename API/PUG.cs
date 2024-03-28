using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ChemStoreWebApp.PUG
{
	public static class REST
	{
		private static readonly HttpClient client = new() { BaseAddress=new Uri("https://pubchem.ncbi.nlm.nih.gov/rest/pug/") };

        public static async Task<int[]> GetCIDListAsync(string searchParam)
        {
            string src = $"compound/name/{searchParam}/cids/txt";
            string response;

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

        public struct Chemical
        {
            public int CID;
            public string CASNumber;
            public string CommonName;
            public string[] Synonyms;
            public string[] MolecularFormulas;
            public string MolecularWeightValue;
            public string MolecularWeightUnit;
            public string MolecularWeightCombo;
            public string PubChemStorageCondition;
            public string[] HCodes;
            public string PCodes;
            public string[] HazardIconURLs;
        }

        public static async Task<Chemical> GetChemicalAsync(int CID)
        {
            string src = $"data/compound/{CID}/JSON";

            string response = await client.GetStringAsync(src);

            JsonNode node = JsonNode.Parse(response);

            //fill our struct
            Chemical chemicalData = new()
            {
                CID = GetCID(node),
                CASNumber = GetCASNumber(node),
                CommonName = GetCommonName(node),
                Synonyms = GetSymonoms(node),
                MolecularFormulas = GetMolecularFormulas(node),
                MolecularWeightValue = GetMolecularWeightValue(node).ToString("0.##"), // This will round to 2 decimal places
                MolecularWeightUnit = GetMolecularWeightUnit(node),
                MolecularWeightCombo = GetMolecularWeight(node),
                PubChemStorageCondition = GetPubChemStorageCondition(node),
                HCodes = GetHCodes(node),
                PCodes = GetPCodes(node),
                HazardIconURLs = GetHazardIconURLs(node),
            };

            return chemicalData;
        }

        private static JsonNode GetSection(JsonNode node, string sectionHeading)
        {
            JsonArray arr = node.AsArray();
            for (int i = 0; i < arr.Count; i++)
                if (arr[i]["TOCHeading"].Deserialize<string>() == sectionHeading) return arr[i];

            return null;
        }

        private static int GetCID(JsonNode obj)
        {
            return obj["Record"]["RecordNumber"].Deserialize<int>();
        }

        private static string[] GetHazardIconURLs(JsonNode obj)
        {
            // Try/Catch in case the chemical doesn't have this property
            try
            {
                // Navigate to Hazard Icon URLs node
                JsonNode chemicalSafetyNode = GetSection(obj["Record"]["Section"], "Chemical Safety");
                JsonNode allURLsNode = chemicalSafetyNode["Information"][0]["Value"]["StringWithMarkup"][0]["Markup"];

                // Copy URLs from original array to new ArrayList
                ArrayList urls = new();
                foreach (JsonNode node in allURLsNode.AsArray())
                    urls.Add(node["URL"].ToString());

                // Return the new ArrayList of URLs as a string array
                return (string[])urls.ToArray(typeof(string));
            }
            catch (NullReferenceException)
            {
                return Array.Empty<string>();
            }
        }

        private static string[] GetMolecularFormulas(JsonNode obj)
        {
            // Try/Catch in case the chemical doesn't have this property
            try
            {
                // Navigate to Molecular Formulas node
                JsonNode namesNode = GetSection(obj["Record"]["Section"], "Names and Identifiers");
                JsonNode allFormulasNode = GetSection(namesNode["Section"], "Molecular Formula")["Information"];

                // Copy formulas from original array to new HashSet
                HashSet<string> formulas = new(); // Store in a set to remove duplicates
                foreach (JsonNode node in allFormulasNode.AsArray())
                    formulas.Add(node["Value"]["StringWithMarkup"][0]["String"].ToString());

                // Return the new HashSet of formulas as a string array
                string[] formulasArray = new string[formulas.Count];
                formulas.CopyTo(formulasArray);
                return formulasArray;
            }
            catch (NullReferenceException)
            {
                return Array.Empty<string>();
            }
        }

        private static string GetCASNumber(JsonNode obj)
        {
            // Try/Catch in case the chemical doesn't have any CAS numbers (is this even possible?)
            try
            {
                // Navigate to CAS Numbers node
                JsonNode namesNode = GetSection(obj["Record"]["Section"], "Names and Identifiers");
                JsonNode otherIdentifiersNode = GetSection(namesNode["Section"], "Other Identifiers");
                JsonNode allCASNumbersNode = GetSection(otherIdentifiersNode["Section"], "CAS")["Information"];

                // Use a Dictionary<CAS, count> to count how many references each distinct CAS number has 
                Dictionary<string, int> casCountDict = new();
                foreach (JsonNode node in allCASNumbersNode.AsArray())
                {
                    string cas = node["Value"]["StringWithMarkup"][0]["String"].ToString();
                    casCountDict.TryGetValue(cas, out int count); // Default value for int is 0
                    casCountDict[cas] = count++;
                }

                // Return the CAS number with the most references
                return casCountDict.MaxBy(kvp => kvp.Value).Key;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        private static string GetCommonName(JsonNode obj)
        {
            return obj["Record"]["RecordTitle"].Deserialize<string>();
        }

        private static int GetSectionID(JsonNode obj, string TOCHeading)
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

        private static string[] GetSymonoms(JsonNode obj)
        {
            // Declerations
            int i, size;
            int nameSectionID = GetSectionID(obj, "Names and Identifiers"); // Get the "Names and Identifiers" section ID
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

        private static double GetMolecularWeightValue(JsonNode obj)
        {
            int chemPropertiesSectionID = GetSectionID(obj, "Chemical and Physical Properties");  // Get the "Chemical and Physical Properties" sectionId

            //Console.WriteLine(chemPropertiesSectionID);

            return double.Parse(obj["Record"]["Section"][chemPropertiesSectionID]["Section"][0]["Section"][0]["Information"][0]["Value"]["StringWithMarkup"][0]["String"].ToString());
        }

        private static string GetMolecularWeightUnit(JsonNode obj)
        {
            int chemPropertiesSectionID = GetSectionID(obj, "Chemical and Physical Properties");  // Get the "Chemical and Physical Properties" sectionId

            return obj["Record"]["Section"][chemPropertiesSectionID]["Section"][0]["Section"][0]["Information"][0]["Value"]["Unit"].ToString();
        }

        private static string GetMolecularWeight(JsonNode obj)
        {
            // Declerations & Initializations
            double value = GetMolecularWeightValue(obj);
            string unit = GetMolecularWeightUnit(obj);

            string combo = "" + value + " " + unit;

            return combo;
        }
    
        private static string GetPubChemStorageCondition(JsonNode obj)
        {
            // Try/Catch in case the chemical doesn't have this property
            try
            {
                // Navigate to Storage Conditions node
                JsonNode safetyAndHazardsNode = GetSection(obj["Record"]["Section"], "Safety and Hazards");
                JsonNode handlingAndStorageNode = GetSection(safetyAndHazardsNode["Section"], "Handling and Storage");
                JsonNode storageConditionsNode = GetSection(handlingAndStorageNode["Section"], "Storage Conditions");

                return storageConditionsNode["Information"][0]["Value"]["StringWithMarkup"][0]["String"].ToString();
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        private static string[] GetHCodes(JsonNode obj)
        {
            // Try/Catch in case the chemical doesn't have this property
            try
            {
                // Navigate to H Codes array node
                JsonNode safetyAndHazardsNode = GetSection(obj["Record"]["Section"], "Safety and Hazards");
                JsonNode ghsClassificationNode = GetSection(safetyAndHazardsNode["Section"], "Safety and Hazards");
                JsonNode ghsHazardsNode = GetSection(ghsClassificationNode["Section"], "GHS Hazard Statements");
                JsonNode hCodesArrayNode = ghsHazardsNode["Information"][2]["Value"]["StringWithMarkup"];

                int size = hCodesArrayNode.AsArray().Count; //get size so we can iterate through
                string[] hCodes = new string[size]; //declare string array of size

                for (int i = 0; i < size; i++)
                    hCodes[i] = hCodesArrayNode[i]["String"].ToString(); //save each hCode from our json

                return hCodes;
            }
            catch (NullReferenceException)
            {
                return Array.Empty<string>();
            }
        }

        private static string GetPCodes(JsonNode obj)
        {
            // Try/Catch in case the chemical doesn't have this property
            try
            {
                // Navigate to GHS Hazards node
                JsonNode safetyAndHazardsNode = GetSection(obj["Record"]["Section"], "Safety and Hazards");
                JsonNode ghsClassificationNode = GetSection(safetyAndHazardsNode["Section"], "Safety and Hazards");
                JsonNode ghsHazardsNode = GetSection(ghsClassificationNode["Section"], "GHS Hazard Statements");

                return ghsHazardsNode["Information"][3]["Value"]["StringWithMarkup"][0]["String"].ToString();
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}   