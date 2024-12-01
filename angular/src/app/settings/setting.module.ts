import { NgModule } from '@angular/core';
import { PageModule } from '@abp/ng.components/page';
import { SharedModule } from '../shared/shared.module';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { SettingComponent } from './setting.component';
import { SettingRoutingModule } from './setting-routing.module';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { InputNumberModule } from 'primeng/inputnumber';
import { MessagesModule } from 'primeng/messages';
import { SkeletonModule } from 'primeng/skeleton';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { TwoFactorSetupModalComponent } from './2fa/two-factor.component';
import { DialogModule } from 'primeng/dialog';
import { StepperModule } from 'primeng/stepper';
import { InputOtpModule } from 'primeng/inputotp';

@NgModule({
  declarations: [SettingComponent, TwoFactorSetupModalComponent],
  imports: [
    SharedModule,
    SettingRoutingModule,
    PageModule,
    CommonModule,
    CardModule,
    AvatarModule,
    AvatarGroupModule,
    InputNumberModule,
    MessagesModule,
    SkeletonModule,
    IconFieldModule,
    InputIconModule,
    InputTextModule,
    ButtonModule,
    InputGroupModule,
    InputGroupAddonModule,
    DialogModule,
    StepperModule,
    InputOtpModule,
  ],
})
export class SettingModule {}
