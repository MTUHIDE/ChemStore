from django.shortcuts import render, redirect
from django.core.paginator import Paginator
from django.conf import settings

from . import models
from .forms import FilterForm, FilterAdminUser

# data = {'admin' : True}

# Ensure minimum roles exist (`Developer` in debug, `User` and `Admin` in production)
def validate_roles(request):
    req_roles = [ "Developer" ] if settings.DEBUG else [ "User", "Admin" ]
    for role_name in req_roles:
        if len(models.Role.objects.filter(name=role_name)) == 0:
            models.Role.objects.create(name=role_name)

# Checks if the user is in the database, adds them if they're not, and returns the user's row from the db
def get_user(request):
    # Make sure necessary roles exist
    validate_roles(request)

    users = models.User.objects

    if settings.DEBUG:
        # If User table is empty, create & return a dev user object. Otherwise, return existing dev user object
        if users.count() == 0:
            role = models.Role.objects.get(name="Developer")
            return users.create(id=0, name="Dev User", email="dev@dev.dev", role=role)
        else:
            return users.get(id=0)

    else:
        # Get user attributes from session
        attr = request.session.get("attributes", {})

        # Get UID from session attributes
        uid = attr.get("uid")
        if not uid:
            raise Exception("UID not found in session attributes.")

        # If UID is not in User table, create & return an object for the user. Otherwise, return user object
        if len(users.filter(id=uid)) == 0:
            # Get preferred full name and email from session attributes
            name = attr.get("displayName")
            email = attr.get("mail")
            if not name:
                raise Exception("Display Name not found in session attributes.")
            if not email:
                raise Exception("Email not found in session attributes.")

            # Get default role
            role = models.Role.objects.get(name="User")

            # Create & return user object from user's data
            return users.create(id=uid, name=name, email=email, role=role)
        else:
            # Return user object
            return users.get(id=uid)

def get_permissions(request):
    data = {}
    data["user"] = get_user(request)

    if data["user"].role.name != "Developer":
        data["pages"] = ["Index", "Contact", "Log", "Admin", "Debug", "Privacy"]
    elif data["user"].role.name == "SuperAdmin":
        data["pages"] = ["Index", "Contact", "Log", "Admin", "Debug", "Privacy"]
    elif data["user"].role.name == "Admin":
        data["pages"] = ["Index", "Contact", "Log", "Admin", "Privacy"]
    elif data["user"].role.name == "User":
        data["pages"] = ["Index", "Contact", "Privacy"]
    return

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
    data["user"] = get_user(request)

    data["admin"] = False   # Temp admin variable for base.html if statements

    return render(request, "store/index.html", data)


def log(request):
    # Prevent non-admin user from entering/typing page URL
    data = {}
    data["user"] = get_user(request)

    if data["user"].role.name != "Developer":       # Temp if statement figuring out how to implement with roles
        # User is not an admin role
        return redirect('store:index') # store = app_name, index = urlname

    return render(request, "store/log.html", data)


def contact(request):
    data = {
        "email": "chemstores@mtu.edu",
    }
    data["user"] = get_user(request)
    return render(request, "store/contact.html", data)


def admin_index(request):
    # Prevent non-admin user from entering/typing page URL
    data = {}
    data["user"] = get_user(request)

    if data["user"].role.name != "Developer":       # Temp if statement figuring out how to implement with roles
        # User is not an admin role
        return redirect('store:index') # store = app_name, index = urlname

    return render(request, "store/admin/index.html", data)


def admin_location(request):
    # Prevent non-admin user from entering/typing page URL
    data = {"model": models.Location.objects.all()}
    data["user"] = get_user(request)

    if data["user"].role.name != "Developer":  # Temp if statement figuring out how to implement with roles
        # User is not an admin role
        return redirect('store:index')  # store = app_name, index = urlname

    return render(request, "store/admin/location.html", data)


def admin_user(request):
    # Prevent non-admin user from entering/typing page URL
    data = {
        "roles": models.Role.objects.all(),  # Send role model into HTML
        "departments": models.Department.objects.all()  # Send department model into HTML
    }
    data["user"] = get_user(request)

    if data["user"].role.name != "Developer":  # Temp if statement figuring out how to implement with roles
        # User is not an admin role
        return redirect('store:index')  # store = app_name, index = urlname

    users = models.User.objects.all()

    if request.method == "POST":
        action = request.POST.get("action")

        if action == "filter":
            form = FilterAdminUser(request.POST)
            if form.is_valid():
                if form.cleaned_data["name"]:
                    users = users.filter(name__icontains=form.cleaned_data["name"])

                if form.cleaned_data["role"]:
                    users = users.filter(role=form.cleaned_data["role"])
                if form.cleaned_data["department"]:
                    users = users.filter(department=form.cleaned_data["department"])

                data["model"] = users

        # elif action == "edit":

    else:
        data["model"] = users

    return render(request, "store/admin/user.html", data)


def admin_department(request):
    # Prevent non-admin user from entering/typing page URL
    data = {"model": models.Department.objects.all()}
    data["user"] = get_user(request)

    if data["user"].role.name != "Developer":  # Temp if statement figuring out how to implement with roles
        # User is not an admin role
        return redirect('store:index')  # store = app_name, index = urlname

    if request.method == "POST":
        dept_name = request.POST.get("name")  # "name" matches form field
        if dept_name:                         # Don’t allow blank submissions
            models.Department.objects.create(name=dept_name)    # Add name to table from post request

    return render(request, "store/admin/department.html", data)


def admin_role(request):
    # Prevent non-admin user from entering/typing page URL
    data = {
        "model": models.Role.objects.all()
    }
    data["user"] = get_user(request)

    if data["user"].role.name != "Developer":  # Temp if statement figuring out how to implement with roles
        # User is not an admin role
        return redirect('store:index')  # store = app_name, index = urlname

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
    data = {"models": list(debug_models)}
    data["user"] = get_user(request)

    if data["user"].role.name != "Developer":       # Temp if statement figuring out how to implement with roles
        # User is not an admin role
        return redirect('store:index') # store = app_name, index = urlname

    return render(request, "store/debug/index.html", data)


def debug_subpage(request, model_slug):
    # Prevent non-admin user from entering/typing page URL
    model_name = model_slug.replace("-", " ").title()
    data = {
        "model_str": model_name.replace(" ", ""),
        "model": debug_models[model_name].objects.all()
    }
    data["user"] = get_user(request)

    if data["user"].role.name != "Developer":  # Temp if statement figuring out how to implement with roles
        # User is not an admin role
        return redirect('store:index')  # store = app_name, index = urlname

    return render(request, f"store/debug/{model_slug}.html", data)


def privacy(request):
    data = {}
    data["user"] = get_user(request)
    return render(request, "store/privacy.html", data)
