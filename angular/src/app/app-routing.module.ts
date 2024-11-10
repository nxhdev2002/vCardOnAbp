import { permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'card',
    pathMatch: 'full',
    loadChildren: () => import('./card/card.module').then(m => m.CardModule),
    canActivate: [permissionGuard],
    data: {
      requiredPolicy: 'Card.View'
    }
  },
  {
    path: 'card/:id',
    pathMatch: 'full',
    loadChildren: () => import('./card-details/card-details.module').then(m => m.CardDetailsModule),
    canActivate: [permissionGuard],
    data: {
      requiredPolicy: 'Card.View'
    }
  },
  {
    path: 'bin',
    pathMatch: 'full',
    loadChildren: () => import('./bins/bin.module').then(m => m.BinModule),
    canActivate: [permissionGuard],
    data: {
      requiredPolicy: 'Bin.View'
    }
  },
  {
    path: 'payment',
    pathMatch: 'full',
    loadChildren: () => import('./payments/payment.module').then(m => m.PaymentModule),
    canActivate: [permissionGuard],
    data: {
      requiredPolicy: 'Payment.View'
    }
  },
  {
    path: 'currency',
    pathMatch: 'full',
    loadChildren: () => import('./currencies/currency.module').then(m => m.CurrencyModule),
    canActivate: [permissionGuard],
    data: {
      requiredPolicy: 'Currency.View'
    }
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(m => m.AccountModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'tenant-management',
    loadChildren: () =>
      import('@abp/ng.tenant-management').then(m => m.TenantManagementModule.forLazy()),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
