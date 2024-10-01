import { NgModule } from '@angular/core';
import { PageModule } from '@abp/ng.components/page';
import { SharedModule } from '../shared/shared.module';
import { CardComponent } from './card.component';
import { CardRoutingModule } from './card-routing.module';
import { TableModule } from 'primeng/table';

@NgModule({
  declarations: [CardComponent],
  imports: [SharedModule, CardRoutingModule, PageModule, TableModule],
})
export class CardModule {}
