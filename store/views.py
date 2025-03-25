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


debug_models = {
    "Container": models.Container,
    "Container Chemicals": models.ContainerChemicals,
    # "Container Hazards": models.ContainerHazards,
    "Department": models.Department,
    "Hazard Pictogram": models.HazardPictogram,
    # "Hazard Precaution": models.HazardPrecaution,
    "Hazard Statement": models.HazardStatement,
    "Location": models.Location,
    "Location Attribute": models.LocationAttribute,
    # "Log": models.Log,
    "Precautionary Statement": models.PrecautionaryStatement,
    # "Role": models.Role,
    # "Role Permissions": models.RolePermissions,
    # "Statement Pictogram": models.StatementPictogram,
    # "User": models.User,
}


def debug_index(request):
    return render(request, "store/debug/index.html", {
        "models": list(debug_models)
    })


def debug_subpage(request, model_slug):
    model_name = model_slug.replace("-", " ").title()
    data = {
        "model_str": model_name.replace(" ", ""),
        "model": debug_models[model_name].objects.all()
    }
    return render(request, f"store/debug/{model_slug}.html", data)


def privacy(request):
    data = {}
    return render(request, "store/privacy.html", data)
