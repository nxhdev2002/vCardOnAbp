import { NgModule } from '@angular/core';
import { PageModule } from '@abp/ng.components/page';
import { SharedModule } from '../../shared/shared.module';
import { CardManagementComponent } from './card-management.component';
import { CardManagementRoutingModule } from './card-management-routing.module';
import { TableModule } from 'primeng/table';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { MenuModule } from 'primeng/menu';
import { DialogModule } from 'primeng/dialog';
import { FundCardModalComponent } from 'src/app/shared/components/fund-cards/fund-card.component';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { NoteCardModalComponent } from 'src/app/shared/components/note-cards/note-card.component';

@NgModule({
  declarations: [
    CardManagementComponent,
    FundCardModalComponent,
    NoteCardModalComponent
  ],
  imports: [
    SharedModule,
    InputTextareaModule,
    InputNumberModule,
    DialogModule,
    CardManagementRoutingModule,
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
export class CardManagementModule {}
