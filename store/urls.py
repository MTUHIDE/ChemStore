from django.urls import path

from . import views

app_name = 'store'

urlpatterns = [
    path('', views.index, name='index'),
    path('log', views.log, name='log'),
    path('contact', views.contact, name='contact'),
    path('admin', views.admin, name='admin'),
    path('debug', views.debug_index, name='debug'),
    path('debug/<str:model_url>', views.debug_subpage, name='debug_subpage'),
    path('privacy', views.privacy, name='privacy'),
]
