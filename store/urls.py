from django.urls import path

from . import views

app_name = 'store'

urlpatterns = [
    path('', views.index, name='index'),
    path('log', views.log, name='log'),
    path('contact', views.contact, name='contact'),
    path('admin', views.admin_index, name='admin'),
    path('admin/<slug:model_slug>', views.admin_subpage, name='admin_subpage'),
    path('debug', views.debug_index, name='debug'),
    path('debug/<slug:model_slug>', views.debug_subpage, name='debug_subpage'),
    path('privacy', views.privacy, name='privacy'),
]
