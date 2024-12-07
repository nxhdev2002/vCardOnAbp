import { ABP, RoutesService, eLayoutType } from '@abp/ng.core';
import { eThemeSharedRouteNames } from '@abp/ng.theme.shared';
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
        path: '/payment',
        name: '::Payment',
        iconClass: 'pi pi-dollar',
        order: 1,
        requiredPolicy: 'Payment.View',
        layout: eLayoutType.application,
      },
      {
        path: '/bin',
        name: '::Bin',
        iconClass: 'pi pi-cart-plus',
        order: 2,
        requiredPolicy: 'Bin.View',
        layout: eLayoutType.application,
      },
      {
        path: '/currency',
        name: '::Currency',
        iconClass: 'pi pi-dollar',
        order: 2,
        requiredPolicy: 'Currency.View',
        layout: eLayoutType.application,
      },
      {
        path: '/card',
        name: '::Card',
        iconClass: 'pi pi-credit-card',
        order: 3,
        requiredPolicy: 'Card.View',
        layout: eLayoutType.application,
      },
      {
        path: '/transactions',
        name: '::Transaction',
        iconClass: 'pi pi-credit-card',
        order: 3,
        requiredPolicy: 'Card.View',
        layout: eLayoutType.application,
      },
      {
        path: '/settings',
        name: '::Setting',
        iconClass: 'fa fa-wrench',
        order: 4,
        requiredPolicy: 'Payment.View',
        layout: eLayoutType.application,
      },
      // Admin
      {
        path: '/admin/cards',
        name: '::Card',
        iconClass: 'pi pi-credit-card',
        order: 0,
        requiredPolicy: 'Card.View',
        parentName: eThemeSharedRouteNames.Administration,
        layout: eLayoutType.application,
      },
    ]);
  };
}
