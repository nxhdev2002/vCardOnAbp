import { NgModule } from '@angular/core';
import { PageModule } from '@abp/ng.components/page';
import { SharedModule } from '../shared/shared.module';
import { TableModule } from 'primeng/table';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { PaymentComponent } from './payment.component';
import { PaymentRoutingModule } from './payment-routing.module';
import { TagModule } from 'primeng/tag';

@NgModule({
  declarations: [PaymentComponent],
  imports: [SharedModule, PaymentRoutingModule, PageModule, TableModule, InputTextModule, InputIconModule, CommonModule, ButtonModule, DialogModule, TagModule],
})
export class PaymentModule {}
