import { NgModule } from "@angular/core";
import { BinsComponent } from "./bins.component";
import { PageModule } from "@abp/ng.components/page";
import { SharedModule } from "primeng/api";
import { TableModule } from "primeng/table";
import { BinRoutingModule } from "./bin-routing.module";

@NgModule({
  declarations: [BinsComponent],
  imports: [SharedModule, BinRoutingModule, PageModule, TableModule],
})
export class BinModule {}
