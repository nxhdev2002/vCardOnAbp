import { CoreModule } from '@abp/ng.core';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { FundCardModalComponent } from './components/fund-cards/fund-card.component';
import { NoteCardModalComponent } from './components/note-cards/note-card.component';

@NgModule({
  declarations: [FundCardModalComponent, NoteCardModalComponent],
  imports: [
    CoreModule,
    ThemeSharedModule,
    NgbDropdownModule,
    NgxValidateCoreModule
  ],
  exports: [
    CoreModule,
    ThemeSharedModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
    FundCardModalComponent,
    NoteCardModalComponent
  ],
  providers: []
})
export class SharedModule {}
