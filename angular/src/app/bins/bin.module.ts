import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from "@angular/core";
import { BinsComponent } from "./bins.component";
import { PageModule } from "@abp/ng.components/page";
import { SharedModule } from "primeng/api";
import { BinRoutingModule } from "./bin-routing.module";
import { CardModule } from 'primeng/card';
import { ButtonModule } from "primeng/button";
import { CommonModule } from "@angular/common";
import { LocalizationModule } from "@abp/ng.core";
import { DataViewModule } from 'primeng/dataview';
import { TagModule } from 'primeng/tag';
import { DialogModule } from 'primeng/dialog';
import { FormsModule } from "@angular/forms";

@NgModule({
  declarations: [BinsComponent],
  imports: [SharedModule, BinRoutingModule, PageModule, DataViewModule, CardModule, ButtonModule, CommonModule, LocalizationModule, TagModule, DialogModule, FormsModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class BinModule {}
