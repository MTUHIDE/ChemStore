from django.shortcuts import render

from . import models


def index(request):
    # Everything in the dictionary can be referenced by the template
    data = {
        "greeting": "Hello world!"
    }
    return render(request, "store/index.html", data)


def log(request):
    data = {}
    return render(request, "store/log.html", data)


def contact(request):
    data = {
        "email": "jeholtre@mtu.edu",
    }
    return render(request, "store/contact.html", data)


def admin(request):
    data = {}
    return render(request, "store/admin.html", data)


debug_models = [
    ("container", "Container", models.Container),
    ("container_chemicals", "ContainerChemicals", models.ContainerChemicals),
    # ("container_hazards", "ContainerHazards", models.ContainerHazards),
    ("department", "Department", models.Department),
    ("hazard_pictogram", "HazardPictogram", models.HazardPictogram),
    # ("hazard_precaution", "HazardPrecaution", models.HazardPrecaution),
    ("hazard_statement", "HazardStatement", models.HazardStatement),
    ("location", "Location", models.Location),
    ("location_attribute", "LocationAttribute", models.LocationAttribute),
    # ("log", "Log", models.Log),
    ("precautionary_statement", "PrecautionaryStatement",
     models.PrecautionaryStatement),
    # ("role", "Role", models.Role),
    # ("role_permissions", "RolePermissions", models.RolePermissions),
    # ("statement_pictogram", "StatementPictogram", models.StatementPictogram),
    # ("user", "User", models.User),
]


def debug_index(request):
    return render(request, "store/debug/index.html", {
        "models": [(x[0], x[1]) for x in debug_models]
    })


def debug_subpage(request, model_url):
    model = [x[2] for x in debug_models if x[0] == model_url][0]
    return render(request, f"store/debug/{model_url}.html", {
        "model": model.objects.all()
    })


def privacy(request):
    data = {}
    return render(request, "store/privacy.html", data)
