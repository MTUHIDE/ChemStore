from django.forms import Form, CharField, TextInput, SelectMultiple, ModelChoiceField

from .models import Department


class FilterForm(Form):
    container_name = CharField(required=False,
                               label="",
                               widget=TextInput(attrs={"placeholder": "Container"}))

    location = CharField(required=False,
                         label="",
                         widget=TextInput(attrs={"placeholder": "Location"}))

    hazard = CharField(required=False,
                       label="",
                       widget=TextInput(attrs={"placeholder": "Hazard"}))

    cas_number = CharField(required=False,
                           label="",
                           widget=TextInput(attrs={"placeholder": "CAS Number"}))

    department = ModelChoiceField(required=False,
                                  label="",
                                  queryset=Department.objects.all(),
                                  empty_label="Select a department")
