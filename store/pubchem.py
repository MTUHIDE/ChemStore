class PubChem:
    """
    API wrapper for PubChem's PUG REST API: https://pubchem.ncbi.nlm.nih.gov/docs/pug-rest
    """
    def __init__(self):
        self.base_url = "https://pubchem.ncbi.nlm.nih.gov/rest/pug"

    def _get(self, path, params=None):
        pass
