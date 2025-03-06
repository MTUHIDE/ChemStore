from django.shortcuts import render

def index(request):
    # Everything in the dictionary can be referenced by the template
    data = {
        "greeting": "Hello world!"
    }
    return render(request, "store/index.html", data)
