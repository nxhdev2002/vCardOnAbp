import { NgModule } from '@angular/core';
import { PageModule } from '@abp/ng.components/page';
import { SharedModule } from '../shared/shared.module';
import { TableModule } from 'primeng/table';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { CurrencyComponent } from './currency.component';
import { CurrencyRoutingModule } from './currency-routing.module';
import { DialogModule } from 'primeng/dialog';

@NgModule({
  declarations: [CurrencyComponent],
  imports: [SharedModule, CurrencyRoutingModule, PageModule, TableModule, InputTextModule, InputIconModule, CommonModule, ButtonModule, DialogModule],
})
export class CurrencyModule {}
