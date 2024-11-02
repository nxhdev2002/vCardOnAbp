import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/bin',
        name: '::Bin',
        iconClass: 'pi pi-cart-plus',
        order: 2,
        layout: eLayoutType.application,
      },
      {
        path: '/currency',
        name: '::Currency',
        iconClass: 'pi pi-dollar',
        order: 2,
        layout: eLayoutType.application,
      },
      {
        path: '/card',
        name: '::Card',
        iconClass: 'pi pi-credit-card',
        order: 3,
        layout: eLayoutType.application,
      },
    ]);
  };
}
