import { NgModule } from "@angular/core";
import { BinsComponent } from "./bins.component";
import { PageModule } from "@abp/ng.components/page";
import { SharedModule } from "primeng/api";
import { TableModule } from "primeng/table";
import { BinRoutingModule } from "./bin-routing.module";
import { CardModule } from 'primeng/card';
import { ButtonModule } from "primeng/button";
import { CommonModule } from "@angular/common";
import { LocalizationModule } from "@abp/ng.core";

@NgModule({
  declarations: [BinsComponent],
  imports: [SharedModule, BinRoutingModule, PageModule, TableModule, CardModule, ButtonModule, CommonModule, LocalizationModule],
})
export class BinModule {}
