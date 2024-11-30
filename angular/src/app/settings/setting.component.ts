import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { LocalizationService } from '@abp/ng.core';
import { Message } from 'primeng/api';
import { AccountsService } from '@proxy/controllers';
import { ProfileInfoDto } from '@proxy/accounts/dtos';

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.less'],
  providers: [DatePipe],
})
export class SettingComponent implements OnInit {
  profileInfo: ProfileInfoDto | undefined;

  unverifiedEmailMessage: Message[];
  unset2FAMessage: Message[];

  loading: boolean = true;      
  constructor(private _localizationService: LocalizationService, private _accountService: AccountsService) {}

  ngOnInit() {
    this.getProfileInfo();
    this.unverifiedEmailMessage = [{ severity: 'warn', detail: this._localizationService.instant('::UnverifiedEmailMessage') }];
    this.unset2FAMessage = [{ severity: 'warn', detail: this._localizationService.instant('::Unset2FAMessage') }];
  }

 getProfileInfo() {
  this.loading = true;
  this._accountService.getProfile().subscribe(res => {
    this.loading = false;
    this.profileInfo = res;
  });
 }
}
