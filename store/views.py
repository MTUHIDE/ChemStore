from django.shortcuts import render

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
    data = {}
    return render(request, "store/contact.html", data)

def admin(request):
    data = {}
    return render(request, "store/admin.html", data)

def debug(request):
    data = {}
    return render(request, "store/debug.html", data)

def privacy(request):
    data = {}
    return render(request, "store/privacy.html", data)
