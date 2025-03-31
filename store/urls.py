from django.urls import path

from . import views

app_name = 'store'

urlpatterns = [
    path('', views.index, name='index'),
    path('log', views.log, name='log'),
    path('contact', views.contact, name='contact'),
    path('admin', views.admin_index, name='admin'),
    path('admin/location', views.admin_location, name='admin_location'),
    path('admin/user', views.admin_user, name='admin_user'),
    path('admin/department', views.admin_department, name='admin_department'),
    path('admin/role', views.admin_role, name='admin_role'),
    path('debug', views.debug_index, name='debug'),
    path('debug/<slug:model_slug>', views.debug_subpage, name='debug_subpage'),
    path('privacy', views.privacy, name='privacy'),
]
