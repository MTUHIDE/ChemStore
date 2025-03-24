from django.shortcuts import render

from .models import Container, ContainerChemicals, Location, LocationAttribute, Department, HazardStatement, PrecautionaryStatement, HazardPictogram


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


def debug_index(request):
    data = {}
    return render(request, "store/debug/index.html", data)


def debug_container(request):
    containers = Container.objects.all()
    return render(request, "store/debug/container.html", {
        "containers": containers
    })


def debug_container_chemicals(request):
    container_chemicals = ContainerChemicals.objects.all()
    return render(request, "store/debug/container_chemicals.html", {
        "container_chemicals": container_chemicals
    })


def debug_location(request):
    location = Location.objects.all()
    return render(request, "store/debug/location.html", {
        "location": location
    })


def debug_location_attribute(request):
    location_attribute = LocationAttribute.objects.all()
    return render(request, "store/debug/location_attribute.html", {
        "location_attribute": location_attribute
    })


def debug_department(request):
    department = Department.objects.all()
    return render(request, "store/debug/department.html", {
        "department": department
    })


def debug_hazard_statement(request):
    hazard_statement = HazardStatement.objects.all()
    return render(request, "store/debug/hazard_statement.html", {
        "hazard_statement": hazard_statement
    })


def debug_precautionary_statement(request):
    precautionary_statement = PrecautionaryStatement.objects.all()
    return render(request, "store/debug/precautionary_statement.html", {
        "precautionary_statement": precautionary_statement
    })


def debug_hazard_pictogram(request):
    hazard_pictogram = HazardPictogram.objects.all()
    return render(request, "store/debug/hazard_pictogram.html", {
        "hazard_pictogram": hazard_pictogram
    })


def privacy(request):
    data = {}
    return render(request, "store/privacy.html", data)
