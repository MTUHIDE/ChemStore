from django.urls import path

from . import views

app_name = 'store'

urlpatterns = [
    path('', views.index, name='index'),
    path('log', views.log, name='log'),
    path('contact', views.contact, name='contact'),
    path('admin', views.admin, name='admin'),

    path('debug', views.debug_index, name='debug'),
    path('debug/container', views.debug_container, name='debug/container'),
    path('debug/container_chemicals', views.debug_container_chemicals,
         name='debug/container_chemicals'),
    # path('debug/container_hazards', views.debug_container_hazards, name='debug/container_hazards'),
    path('debug/department', views.debug_department, name='debug/department'),
    path('debug/hazard_pictogram', views.debug_hazard_pictogram,
         name='debug/hazard_pictogram'),
    # path('debug/hazard_precaution', views.debug_hazard_precaution, name='debug/hazard_precaution'),
    path('debug/hazard_statement', views.debug_hazard_statement,
         name='debug/hazard_statement'),
    path('debug/location', views.debug_location, name='debug/location'),
    path('debug/location_attribute', views.debug_location_attribute,
         name='debug/location_attribute'),
    # path('debug/log', views.debug_log, name='debug/log'),
    path('debug/precautionary_statement', views.debug_precautionary_statement,
         name='debug/precautionary_statement'),
    # path('debug/role', views.debug_role, name='debug/role'),
    # path('debug/role_permissions', views.debug_role_permissions, name='debug/role_permissions'),
    # path('debug/statement_pictogram', views.debug_statement_pictogram, name='debug/statement_pictogram'),
    # path('debug/user', views.debug_user, name='debug/user'),

    path('privacy', views.privacy, name='privacy'),
]
