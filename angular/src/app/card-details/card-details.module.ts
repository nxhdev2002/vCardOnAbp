import { NgModule } from '@angular/core';
import { PageModule } from '@abp/ng.components/page';
import { SharedModule } from '../shared/shared.module';
import { CardDetailsComponent } from './card-details.component';
import { CardDetailsRoutingModule } from './card-details-routing.module';
import { CommonModule } from '@angular/common';
import { SplitterModule } from 'primeng/splitter';
import { TableModule } from 'primeng/table';
import { CardModule } from 'primeng/card';

@NgModule({
  declarations: [CardDetailsComponent],
  imports: [SharedModule, CardDetailsRoutingModule, PageModule, SplitterModule, CommonModule, TableModule, CardModule],
})
export class CardDetailsModule {}
