import { NgModule } from '@angular/core';
import { PageModule } from '@abp/ng.components/page';
import { SharedModule } from '../shared/shared.module';
import { CardDetailsComponent } from './card-details.component';
import { CardDetailsRoutingModule } from './card-details-routing.module';
import { CommonModule } from '@angular/common';
import { SplitterModule } from 'primeng/splitter';
import { TableModule } from 'primeng/table';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { DialogModule } from 'primeng/dialog';
import { InputNumberModule } from 'primeng/inputnumber';

@NgModule({
  declarations: [CardDetailsComponent],
  imports: [SharedModule, InputNumberModule, CardDetailsRoutingModule, DialogModule, PageModule, SplitterModule, CommonModule, TableModule, CardModule, ButtonModule, ConfirmDialogModule, ToastModule],
})
export class CardDetailsModule {}
