from django.shortcuts import render
from django.core.paginator import Paginator

from . import models
from .forms import FilterForm


def index(request):
    # Everything in the dictionary can be referenced by the template

    containers = models.Container.objects.all().order_by("product_name")  # order by container name by default

    if request.method == "POST":
        form = FilterForm(request.POST)
        if form.is_valid():
            containers = containers.filter(
                product_name__icontains=form.cleaned_data["container_name"] # case-insensitive container name
            )

            if form.cleaned_data["cas_number"]:
                # filter by chemical CAS number
                containers = containers.filter(containerchemicals__chemical_cas__icontains=form.cleaned_data["cas_number"])

            if form.cleaned_data["location"]:
                # filter by location
                containers = containers.filter(location__name__icontains=form.cleaned_data["location"])

            if form.cleaned_data["hazard"]:
                # filter by hazard statement
                # this needs to be separate because if the hazard is blank, it will filter out all containers that have no hazards
                containers = containers.filter(hazards__hazard_code__icontains=form.cleaned_data["hazard"])

            if form.cleaned_data["department"]:
                # filter by department
                # this needs to be separate because if the department is blank, it will filter out all containers
                containers = containers.filter(location__department=form.cleaned_data["department"])

            print(containers.all().count())  # for debugging purposes

    else:
        form = FilterForm()

    paginator = Paginator(containers, 25)

    page_number = request.GET.get("page")
    page_obj = paginator.get_page(page_number)

    data = {
        "filter_form": form,
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


def admin_index(request):
    return render(request, "store/admin/index.html")


def admin_location(request):
    return render(
        request, "store/admin/location.html", {"model": models.Location.objects.all() }
    )


def admin_user(request):
    return render(request, "store/admin/user.html")
    # return render(request, "store/admin/user.html", {"model": models.User.objects.all() })


def admin_department(request):
    return render(
        request, "store/admin/department.html", {"model": models.Department.objects.all() }
    )


def admin_role(request):
    return render(request, "store/admin/role.html")
    # return render(request, "store/admin/role.html", {"model": models.Role.objects.all()})


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
    "Log": models.Log,
    "Precautionary Statement": models.PrecautionaryStatement,
    # "Role": models.Role,
    # "Role Permissions": models.RolePermissions,
    # "Statement Pictogram": models.StatementPictogram,
    "User": models.User,
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
