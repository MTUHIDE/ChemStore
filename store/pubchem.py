import requests


class PubChem:
    """
    API wrapper for PubChem's PUG REST API.
    """

    def __init__(self):
        self.base_url = "https://pubchem.ncbi.nlm.nih.gov/rest/pug"

    def _get(self, path, params=None):
        url = f"{self.base_url}/{path}"

        response = requests.get(
            url,
            params=params,
            timeout=5
        )

        response.raise_for_status()

        return response.json()

    def autocomplete(self, query):
        url = (
            "https://pubchem.ncbi.nlm.nih.gov/rest/autocomplete/"
            f"compound/{requests.utils.quote(query)}/json"
        )

        response = requests.get(url, timeout=5)
        response.raise_for_status()

        data = response.json()

        return data.get("dictionary_terms", {}).get("compound", [])
