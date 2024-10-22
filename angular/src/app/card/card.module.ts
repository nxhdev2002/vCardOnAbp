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

@NgModule({
  declarations: [CardComponent],
  imports: [SharedModule, CardRoutingModule, PageModule, TableModule, InputTextModule, InputIconModule, CommonModule, ButtonModule],
})
export class CardModule {}
