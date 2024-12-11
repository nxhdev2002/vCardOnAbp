import { NgModule } from '@angular/core';
import { PageModule } from '@abp/ng.components/page';
import { SharedModule } from '../shared/shared.module';
import { CardComponent } from './card.component';
import { CardRoutingModule } from './card-routing.module';
import { TableModule } from 'primeng/table';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { DialogModule } from 'primeng/dialog';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { MenuModule } from 'primeng/menu';

@NgModule({
  declarations: [CardComponent],
  imports: [
    SharedModule,
    InputTextareaModule,
    InputNumberModule,
    DialogModule,
    CardRoutingModule,
    PageModule,
    TableModule,
    InputTextModule,
    InputIconModule,
    CommonModule,
    ButtonModule,
    TagModule,
    MenuModule
  ],
})
export class CardModule {}
