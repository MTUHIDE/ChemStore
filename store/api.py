# This file will contain functions to interact with the PubChem PUG REST and PUG
# View APIs. Interacting with the two APIs should be split into different
# functions
# For example, one function should take in a string (name of compound) to get a
# CID list from PUG REST
# Another function should take in a CID and return some info about the compound
# from PUG View
import requests

BASE = "https://pubchem.ncbi.nlm.nih.gov/rest/pug"
PROPS = "MolecularWeight,MolecularFormula,IsomericSMILES,IUPACName"

name = input("Enter Element or compound: ")


def get_pubchem_info(name):
    cid_url = f"{BASE}/compound/name/{name}/cids/JSON"
    cid_response = requests.get(cid_url)

    if cid_response.status_code != 200:
        
        return f"Failed to find information on {name}"

    cid_data = cid_response.json()
    cids = cid_data.get("IdentifierList", {}).get("CID", [])

    if not cids:
        return f"Failed to find information on {name}"

    cid = cids[0]

    prop_url = f"{BASE}/compound/cid/{cid}/property/{PROPS}/JSON"
    prop_response = requests.get(prop_url)

    if prop_response.status_code != 200:
        return f"Failed to find information on {name}"

    data = prop_response.json()
    return data["PropertyTable"]["Properties"][0]

print(get_pubchem_info(name))