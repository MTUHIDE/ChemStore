from django.urls import path

from . import views

app_name = 'store'

urlpatterns = [
    path('', views.index, name='index'),
    path('log', views.log, name='log'),
    path('contact', views.contact, name='contact'),
    path('admin', views.admin, name='admin'),
    path('debug', views.debug, name='debug'),
    path('privacy', views.privacy, name='privacy'),
]
