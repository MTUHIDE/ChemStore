# This file will contain functions to interact with the PubChem PUG REST and PUG
# View APIs. Interacting with the two APIs should be split into different
# functions
# For example, one function should take in a string (name of compound) to get a
# CID list from PUG REST
# Another function should take in a CID and return some info about the compound
# from PUG View
import requests

class Rest: 
    base = "https://pubchem.ncbi.nlm.nih.gov/rest/pug/"
    
    def get_cid_list(search_param):
        src = f"compound/name/{search_param}/cids/txt"
        response = requests.get(Rest.base + src)

        if response.status_code != 200:
            return [-1]
        
        lines = response.text.strip().split('\n')

        if lines[0].startswith("Status"):
            status_split = lines[0].split(" ")
            return [-1, int(status_split[1])]
        
        return [int(line) for line in lines]
class View:
    base = "https://pubchem.ncbi.nlm.nih.gov/rest/pug_view/"
    class Chemical: 
        def __init__(self):
            self.cid = None
            self.CASNumber = None
            self.commonName = None
            self.synonyms = []
            self.molecularFormulas = []
            self.molecularWeight = None
            self.storageConditions = None
            self.HCodes = []
            self.PCodes = None
            self.hazardIconURLs = []
    
    def get_chemical(cid):
        src = f"data/compound/{cid}/JSON"
        response = requests.get(View.base + src)

        node = response.json()

        chemical_data = View.Chemical()

        chemical_data.cid = View.getCID(node)
        chemical_data.CASNumber = View.getCASNumber(node)
        chemical_data.commonName = View.getCommonName(node)
        chemical_data.synonyms = View.getSynonyms(node)
        chemical_data.molecularFormulas = View.getMolecularFormulas(node)
        chemical_data.molecularWeight = View.getMolecularWeight(node)
        chemical_data.storageConditions = View.getStorageCondition(node)
        chemical_data.HCodes = View.getHCodes(node)
        chemical_data.PCodes = View.getPCodes(node)
        chemical_data.hazardIconURLs = View.getHazardIconURLs(node)

        return chemical_data
    
    def getSection(sections, section_heading):
        for section in sections:
            if section.get("TOCHeading") == section_heading:
                return section
        return None
    
    def getCID(obj):
        return obj["Record"]["RecordNumber"]
    
    def getHazardIconURLs(obj):
        try:
            primaryHazardsNode = View.getSection(obj["Record"]["Section"], "Primary Hazards")

            urls = []
            # Loop through all Information entries since there may be multiple
            for info in primaryHazardsNode["Information"]:
                for markup in info["Value"]["StringWithMarkup"]:
                    # Each markup entry may have a "Markup" list containing icon URLs
                    if "Markup" in markup:
                        for icon in markup["Markup"]:
                            if "URL" in icon:
                                urls.append(icon["URL"])

            return urls
        except (KeyError, TypeError, IndexError):
            return []
    
    def getMolecularFormulas(obj):
        try:
                namesNode = View.getSection(obj["Record"]["Section"],"Names and Identifiers")
                allFormulasNode = View.getSection(namesNode["Section"], "Molecular Formula")["Information"]

                formulas = []
                for entry in allFormulasNode:
                    i = entry["Value"]["StringWithMarkup"][0]["String"]
                    if i not in formulas:
                        formulas.append(i)
                return formulas
        
        except (KeyError, TypeError, IndexError):
            return []
        
    
    def getCASNumber(obj):
        try:
            namesNode = View.getSection(obj["Record"]["Section"],"Names and Identifiers")
            otherIdentifiersNode = View.getSection(namesNode["Section"],"Other Identifiers")
            allCASNumbersNode = View.getSection(otherIdentifiersNode["Section"],"CAS")["Information"]

            cas_count = {}
            for entry in allCASNumbersNode:
                cas = entry["Value"]["StringWithMarkup"][0]["String"]
                cas_count[cas] = cas_count.get(cas,0) + 1

            return max(cas_count, key = cas_count.get)

        except (KeyError, TypeError, IndexError):
            return None
    
    def getCommonName(obj):
        return obj["Record"]["RecordTitle"]
    
    def getSectionID(obj, TOCHeading):
        recordSectionID = -1

        for i in range(len(obj["Record"]["Section"])):
            if obj["Record"]["Section"][i]["TOCHeading"] == TOCHeading:
                recordSectionID = i
                break
        return recordSectionID
    
    def getSynonyms(obj):
        nameSectionID = View.getSectionID(obj, "Names and Identifiers")
        topFiveSynonyms = []

        size = len(obj["Record"]["Section"][nameSectionID]["Section"][4]["Section"][1]["Information"][0]["Value"]["StringWithMarkup"])

        allSynonyms = []

        for i in range(size):
            allSynonyms.append(obj["Record"]["Section"][nameSectionID]["Section"][4]["Section"][1]["Information"][0]["Value"]["StringWithMarkup"][i]["String"])

        for i in range(5):
            topFiveSynonyms.append(allSynonyms[i])

        return topFiveSynonyms
    
    def getMolecularWeightValues(obj):
        chemPropertiesSectionID = View.getSectionID(obj,"Chemical and Physical Properties")
        return round(float(obj["Record"]["Section"][chemPropertiesSectionID]["Section"][0]["Section"][0]["Information"][0]["Value"]["StringWithMarkup"][0]["String"]), 2)
    
    def getMolecularWeightUnit(obj):
        chemPropertiesSectionID = View.getSectionID(obj, "Chemical and Physical Properties")
        return obj["Record"]["Section"][chemPropertiesSectionID]["Section"][0]["Section"][0]["Information"][0]["Value"]["Unit"]
    
    def getMolecularWeight(obj):
        value = View.getMolecularWeightValues(obj)
        unit = View.getMolecularWeightUnit(obj)

        combo = str(value) + " " + unit
        return combo
    
    def getStorageCondition(obj):
        try:
            safetyAndHazardsNode = View.getSection(obj["Record"]["Section"],"Safety and Hazards")
            handlingAndStorageNode = View.getSection(safetyAndHazardsNode["Section"],"Handling and Storage")
            storageConditionsNode = View.getSection(handlingAndStorageNode["Section"],"Storage Conditions")
            return storageConditionsNode["Information"][0]["Value"]["StringWithMarkup"][0]["String"]
        except (KeyError, TypeError, IndexError):
            
            return None
    
    def getHCodes(obj):
        try:
            safetyNode      = View.getSection(obj["Record"]["Section"], "Safety and Hazards")
            hazardsIdNode   = View.getSection(safetyNode["Section"], "Hazards Identification")
            ghsNode         = View.getSection(hazardsIdNode["Section"], "GHS Classification")
            
            # Find the first entry specifically named "GHS Hazard Statements"
            # This handles chemicals where index [2] is not always the H codes
            for entry in ghsNode["Information"]:
                if entry.get("Name") == "GHS Hazard Statements":
                    hCodesArrayNode = entry["Value"]["StringWithMarkup"]
                    size   = len(hCodesArrayNode)
                    hCodes = []
                    for i in range(size):
                        hCodes.append(hCodesArrayNode[i]["String"])
                    return hCodes  # Return after the FIRST matching entry only

            return []  # No GHS Hazard Statements found
        except (KeyError, TypeError, IndexError):
            return []
        
    def getPCodes(obj):
        try:
            safetyNode    = View.getSection(obj["Record"]["Section"], "Safety and Hazards")
            hazardsIdNode = View.getSection(safetyNode["Section"], "Hazards Identification")
            ghsNode       = View.getSection(hazardsIdNode["Section"], "GHS Classification")

            # Find the first entry specifically named "Precautionary Statement Codes"
            for entry in ghsNode["Information"]:
                if entry.get("Name") == "Precautionary Statement Codes":
                    return entry["Value"]["StringWithMarkup"][0]["String"]

            return None  # No Precautionary Statement Codes found
        except (KeyError, TypeError, IndexError):
            return None