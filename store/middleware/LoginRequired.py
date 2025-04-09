from django.conf import settings
from django.shortcuts import redirect
from django.urls import reverse


class LoginRequiredMiddleware:
    def __init__(self, get_response):
        self.get_response = get_response

    def __call__(self, request):
        if (not settings.DEBUG
                and request.path != '/accounts/login/'
                and request.path != '/accounts/cas-login/'
                and request.path != '/accounts/logout/'
                and not request.user.is_authenticated):
            return redirect(f"{settings.LOGIN_URL}?next={request.path}")
        response = self.get_response(request)
        return response
