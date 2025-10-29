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
    description = models.CharField(max_length=100)
    pictogram = models.ImageField(null=True) # Null is allowed for testing

# ! Should validate typing and constraints of the tables below with full team at some point
class Log(models.Model):
    id = models.IntegerField(primary_key=True)
    timestamp = models.DateTimeField()
    user_id = models.ForeignKey("User", on_delete=models.SET_NULL, null=True)
    table = models.CharField(max_length=100, null=True)
    key1 = models.CharField(max_length=10, null=True)
    key2 = models.CharField(max_length=10, null=True)
    action = models.TextField(null=True)
    old_value = models.TextField(null=True)
    new_value = models.TextField(null=True)
    description = models.TextField(null=True)
    notes = models.TextField(null=True)

class Role(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.CharField(max_length=100)

class User(models.Model):
    user_id = models.AutoField(primary_key=True)
    name = models.CharField(max_length=100)
    email = models.EmailField(max_length=100)
    role_id = models.ForeignKey("Role", on_delete=models.CASCADE)
    department_id = models.ForeignKey("Department", on_delete=models.CASCADE, null=True)

class RolePermissions(models.Model):
    role_id = models.ForeignKey("Role", on_delete=models.CASCADE)
    location_id = models.ForeignKey("Location", on_delete=models.CASCADE)
    permission = models.TextField(primary_key=True)
    has_perm = models.BooleanField()

class ContainerHazards(models.Model):
    container_id = models.ForeignKey("Container", on_delete=models.CASCADE)
    h_code = models.ForeignKey("HazardStatement", on_delete=models.CASCADE)

class StatementPictogram(models.Model): # Has an extra "id" field in the databse.
    gh_code = models.ForeignKey("HazardPictogram", on_delete=models.CASCADE)
    h_code = models.ForeignKey("HazardStatement", on_delete=models.CASCADE)

class HazardPrecaution(models.Model): # Has an extra "id" field in the database
    h_code = models.ForeignKey("HazardStatement", on_delete=models.CASCADE)
    p_code = models.ForeignKey("PrecautionaryStatement", on_delete=models.CASCADE)
