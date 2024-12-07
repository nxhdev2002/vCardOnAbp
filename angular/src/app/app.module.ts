import { CoreModule, provideAbpCore, withOptions } from '@abp/ng.core';
import { SettingManagementConfigModule } from '@abp/ng.setting-management/config';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IdentityConfigModule } from '@abp/ng.identity/config';
import { AccountConfigModule } from '@abp/ng.account/config';
import { TenantManagementConfigModule } from '@abp/ng.tenant-management/config';
import { registerLocale, storeLocaleData } from '@abp/ng.core/locale';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_ROUTE_PROVIDER } from './route.provider';
import { FeatureManagementModule } from '@abp/ng.feature-management';
import { ThemeLeptonXModule } from '@abp/ng.theme.lepton-x';
import { SideMenuLayoutModule } from '@abp/ng.theme.lepton-x/layouts';
import { AbpOAuthModule } from '@abp/ng.oauth';
import localeVi from '@angular/common/locales/vi';
import { CommonModule, registerLocaleData } from '@angular/common';
import { myDynamicLayouts } from './shared/customlayout';
import { SharedModule } from './shared/shared.module';
import { NavItemModule } from './shared/navitem/nav-item/nav-item.module';

registerLocaleData(localeVi, 'vi');
@NgModule({
  declarations: [AppComponent],
  imports: [
    SharedModule,
    CommonModule,
    NavItemModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule.forRoot({
      environment,
      registerLocaleFn: registerLocale(),
    }),
    AbpOAuthModule.forRoot(),
    AccountConfigModule.forRoot(),
    TenantManagementConfigModule.forRoot(),
    IdentityConfigModule.forRoot(),
    ThemeSharedModule.forRoot(),
    SettingManagementConfigModule.forRoot(),
    ThemeLeptonXModule.forRoot(),
    SideMenuLayoutModule.forRoot(),
    FeatureManagementModule.forRoot(),
  ],
  providers: [APP_ROUTE_PROVIDER, provideAbpCore(
    withOptions({
      dynamicLayouts: myDynamicLayouts,
      environment,
      registerLocaleFn: registerLocale(),
    }),
  ),],
  bootstrap: [AppComponent],
})
export class AppModule {}
