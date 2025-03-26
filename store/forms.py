from django import forms


class FilterForm(forms.Form):
    container_name = forms.CharField(required=False,
                                     label="", widget=forms.TextInput(attrs={"placeholder": "Container"}))
    location = forms.CharField(required=False,
                               label="", widget=forms.TextInput(attrs={"placeholder": "Location"}))
    hazard = forms.CharField(required=False, label="", widget=forms.TextInput(
        attrs={"placeholder": "Hazard"}))
    cas_number = forms.CharField(required=False,
                                 label="", widget=forms.TextInput(attrs={"placeholder": "CAS Number"}))
    # TODO: add department dropdown
