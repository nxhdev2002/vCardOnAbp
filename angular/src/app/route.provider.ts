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
        iconClass: 'fas fa-card',
        order: 2,
        layout: eLayoutType.application,
      },
      {
        path: '/card',
        name: '::Card',
        iconClass: 'fas fa-card',
        order: 3,
        layout: eLayoutType.application,
      },
    ]);
  };
}
