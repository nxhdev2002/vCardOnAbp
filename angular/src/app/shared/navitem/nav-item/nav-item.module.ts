import { NgModule } from '@angular/core';
import { PageModule } from '@abp/ng.components/page';
import { TableModule } from 'primeng/table';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { NavItemsComponent } from './nav-item.component';
import { SharedModule } from '../../shared.module';
import { ToolbarModule } from 'primeng/toolbar';
import { SplitButtonModule } from 'primeng/splitbutton';
import { AvatarModule } from 'primeng/avatar';

@NgModule({
  declarations: [NavItemsComponent],
  imports: [SharedModule, PageModule, TableModule, InputTextModule, InputIconModule, CommonModule, ButtonModule, TagModule, ToolbarModule, SplitButtonModule, AvatarModule],
})
export class NavItemModule {}
