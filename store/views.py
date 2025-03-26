from django.shortcuts import render
from django.core.paginator import Paginator

from . import models
from .forms import FilterForm


def index(request):
    # TODO: Handle filtering & pagination
    # Everything in the dictionary can be referenced by the template
    if request.method == "POST":
        form = FilterForm(request.POST)
        if form.is_valid():
            container_name = request.POST.get("container_name", "")
            location = request.POST.get("location", "")
            hazard = request.POST.get("hazard", "")
            cas_number = request.POST.get("cas_number", "")

            # do the filtering
            containers = models.Container.objects
            if container_name:
                containers = containers.filter(product_name__contains=container_name)
            else:
                containers = containers.all()
            # TODO: the rest of the filter fields
    else:
        form = FilterForm()
        # TODO: do pagination (25 per page)
        containers = models.Container.objects.all()
        # if no page number already, default to 1
        # otherwise go either up or down based on input? idk
        # how does this work

    paginator = Paginator(containers, 25)

    page_number = request.GET.get("page")
    page_obj = paginator.get_page(page_number)

    data = {
        "filter_form": form,
        # "containers": containers,
        "page_obj": page_obj
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
