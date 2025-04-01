from django.db import models


class Container(models.Model):
    container_id = models.AutoField(primary_key=True)
    location = models.ForeignKey("Location", on_delete=models.SET_NULL, null=True)
    product_name = models.CharField(max_length=100)
    size = models.DecimalField(max_digits=5, decimal_places=2)
    notes = models.TextField(blank=True)
    hazards = models.ManyToManyField("HazardStatement", blank=True)


class ContainerChemicals(models.Model):
    container = models.ForeignKey("Container", on_delete=models.CASCADE)
    chemical_cas = models.CharField(max_length=12)
    pubchem_cid = models.IntegerField()
    quantity = models.DecimalField(max_digits=5, decimal_places=2)
    preferred_unit = models.CharField(max_length=10)  # TODO: Switch to a models.Choices field
    state_of_matter = models.CharField(max_length=10)  # TODO: Switch to a models.Choices field
    manufacturer = models.CharField(max_length=100)
    catalog_number = models.CharField(max_length=50)


class Location(models.Model):
    location_id = models.AutoField(primary_key=True)
    parent = models.ForeignKey('self', on_delete=models.CASCADE, null=True)
    level = models.IntegerField(null=True)
    name = models.CharField(max_length=100)
    department = models.ForeignKey("Department", on_delete=models.SET_NULL, null=True)
    # supervisor_id
    is_hidden = models.BooleanField(default=False)
    attributes = models.ManyToManyField("LocationAttribute")

    def __str__(self):
        # include all parent locations in the string
        location = self
        location_string = ""
        while location is not None:
            location_string = location.name + " > " + location_string
            location = location.parent
        return location_string[:-3]  # remove the last " > "


class LocationAttribute(models.Model):
    attribute_id = models.AutoField(primary_key=True)
    value = models.CharField(max_length=100)


class Department(models.Model):
    department_id = models.AutoField(primary_key=True)
    name = models.CharField(max_length=100)

    def __str__(self):
        return self.name


class HazardStatement(models.Model):
    hazard_code = models.CharField(max_length=10, primary_key=True)
    statements = models.TextField()
    hazard_class = models.CharField(max_length=50)  # TODO: Switch to a models.Choices field
    category = models.CharField(max_length=50)  # TODO: Switch to a models.Choices field
    signal_word = models.CharField(max_length=10)  # TODO: Switch to a models.Choices field
    precautionary_statements = models.ManyToManyField("PrecautionaryStatement")
    pictograms = models.ManyToManyField("HazardPictogram")


class PrecautionaryStatement(models.Model):
    precautionary_code = models.CharField(max_length=10, primary_key=True)
    statement = models.TextField()

class HazardPictogram(models.Model):
    pictogram_id = models.AutoField(primary_key=True)
    image = models.ImageField()
    description = models.CharField(max_length=100)

# ! This is a placeholder until we get SSO integration and can tell exactly what fields are needed
class User(models.Model):
    user_id = models.AutoField(primary_key=True)
    name = models.CharField(max_length=100)
    email = models.EmailField(max_length=100)
