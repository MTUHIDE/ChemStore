from django.shortcuts import render, redirect
from django.core.paginator import Paginator
from django.conf import settings

from . import models
from .forms import FilterForm

# data = {'admin' : True}

# Get any necessary user data from the session
def get_user(request):
    data = {}

    if not settings.DEBUG:
        # Try to get the user's short name from the session
        short_name = request.session.get("attributes", {}).get("givenName")
        if not short_name:
            # If not found, throw an error
            raise Exception("Short name not found in session attributes.")
        # If found, add it to the data dictionary
        data["short_name"] = short_name

    return data


def index(request):
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
        "page_obj": page_obj,
    }
    data.update(get_user(request))

    data["admin"] = False   # Temp admin variable for base.html if statements

    return render(request, "store/index.html", data)


def log(request):
    # Prevent non-admin user from entering/typing page URL
    if 0:       # Temp if statement figuring out how to implement with roles
        # User is not an admin role
        return redirect('store:index') # store = app_name, index = urlname

    data = {}
    data.update(get_user(request))

    return render(request, "store/log.html", data)


def contact(request):
    data = {
        "email": "chemstores@mtu.edu",
    }
    data.update(get_user(request))
    return render(request, "store/contact.html", data)


def admin_index(request):
    # Prevent non-admin user from entering/typing page URL
    if 0:       # Temp if statement figuring out how to implement with roles
        # User is not an admin role
        return redirect('store:index') # store = app_name, index = urlname

    data = {}
    data.update(get_user(request))

    return render(request, "store/admin/index.html", data)


def admin_location(request):
    # Prevent non-admin user from entering/typing page URL
    if 0:  # Temp if statement figuring out how to implement with roles
        # User is not an admin role
        return redirect('store:index')  # store = app_name, index = urlname

    data = { "model": models.Location.objects.all() }
    data.update(get_user(request))

    return render(request, "store/admin/location.html", data)


def admin_user(request):
    # Prevent non-admin user from entering/typing page URL
    if 0:  # Temp if statement figuring out how to implement with roles
        # User is not an admin role
        return redirect('store:index')  # store = app_name, index = urlname

    data = {
        "model": models.User.objects.all(),
        "roles": models.Role.objects.all(),         # Send role model into HTML
        "departments": models.Department.objects.all()  # Send department model into HTML
    }
    data.update(get_user(request))

    return render(request, "store/admin/user.html", data)


def admin_department(request):
    # Prevent non-admin user from entering/typing page URL
    if 0:  # Temp if statement figuring out how to implement with roles
        # User is not an admin role
        return redirect('store:index')  # store = app_name, index = urlname

    if request.method == "POST":
        dept_name = request.POST.get("name")  # "name" matches form field
        if dept_name:                         # Don’t allow blank submissions
            models.Department.objects.create(name=dept_name)    # Add name to table from post request

    data = { "model": models.Department.objects.all() }
    data.update(get_user(request))

    return render(request, "store/admin/department.html", data)


def admin_role(request):
    # Prevent non-admin user from entering/typing page URL
    if 0:  # Temp if statement figuring out how to implement with roles
        # User is not an admin role
        return redirect('store:index')  # store = app_name, index = urlname

    data = {
        "model": models.Role.objects.all()
    }
    data.update(get_user(request))

    return render(request, "store/admin/role.html", data)


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
    # Prevent non-admin user from entering/typing page URL
    if 0:       # Temp if statement figuring out how to implement with roles
        # User is not an admin role
        return redirect('store:index') # store = app_name, index = urlname

    data = { "models": list(debug_models) }
    data.update(get_user(request))

    return render(request, "store/debug/index.html", data)


def debug_subpage(request, model_slug):
    # Prevent non-admin user from entering/typing page URL
    if 0:  # Temp if statement figuring out how to implement with roles
        # User is not an admin role
        return redirect('store:index')  # store = app_name, index = urlname

    model_name = model_slug.replace("-", " ").title()
    data = {
        "model_str": model_name.replace(" ", ""),
        "model": debug_models[model_name].objects.all()
    }
    data.update(get_user(request))
    return render(request, f"store/debug/{model_slug}.html", data)


def privacy(request):
    data = {}
    data.update(get_user(request))
    return render(request, "store/privacy.html", data)
