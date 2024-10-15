import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BinsComponent } from './bins.component'

const routes: Routes = [{ path: '', component: BinsComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BinRoutingModule {}
