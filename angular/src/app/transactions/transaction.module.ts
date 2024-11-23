import { NgModule } from '@angular/core';
import { PageModule } from '@abp/ng.components/page';
import { SharedModule } from '../shared/shared.module';
import { TableModule } from 'primeng/table';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { TagModule } from 'primeng/tag';
import { TransactionComponent } from './transaction.component';
import { TransactionRoutingModule } from './transaction-routing.module';

@NgModule({
  declarations: [TransactionComponent],
  imports: [SharedModule, TransactionRoutingModule, PageModule, TableModule, InputTextModule, InputIconModule, CommonModule, ButtonModule, DialogModule, TagModule],
})
export class TransactionModule {}
