import { AuthService, LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { GetCardInput } from '@proxy/cards/dto';
import { CurrencyService } from '@proxy/currencies';
import { CurrencyDto } from '@proxy/currencies/dto';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss'],
})
export class PaymentComponent implements OnInit {
  input: GetCardInput;
  loading: boolean = true;
  currencies!: CurrencyDto[];
      
  visible: boolean = false;

  id: string;
  name: string;
  code: string;
  symbol: string;
  usdRate: number;

  dataViewIdName: string = 'data-view';
  constructor(
    private authService: AuthService, 
    private currencyService: CurrencyService,
    private _toasterService: ToasterService,
    private _localizationService: LocalizationService
  ) {}

  ngOnInit() {
    this.loadCurrencies();
  }

  loadCurrencies() {
    this.loading = true;
    this.currencyService.getList().subscribe((res) => {
      this.loading = false;
      this.currencies = res;
    });
  }

  login() {
    this.authService.navigateToLogin();
  }

  clear() {
    this.id = null;
    this.code = null;
    this.name = null;
    this.symbol = null;
    this.usdRate = null;
  }

  edit(c: CurrencyDto) {
    this.id = c.id;
    this.name = c.name;
    this.code = c.code;
    this.symbol = c.symbol;
    this.usdRate = c.usdRate;

    this.visible = true;
  }

  deleleCurrency(c: CurrencyDto) {
    this.currencyService.delete(c.id).subscribe(() => {
      this._toasterService.success(this._localizationService.instant('::SuccessToast', this._localizationService.instant('::Currency:DeleteCurrency')));
      this.loadCurrencies();
    });
  }

  saveCurrency() {
    this.currencyService.update(this.id, {
      code: this.code,
      name: this.name,
      symbol: this.symbol,
      usdRate: this.usdRate
    }).subscribe(() => {
      this._toasterService.success(this._localizationService.instant('::SuccessToast', this._localizationService.instant('::Currency:UpdateCurrency')));
      this.visible = false;
      this.loadCurrencies();
    });
  }

  addCurrency() {
    this.currencyService.create({
      code: this.code,
      name: this.name,
      symbol: this.symbol,
      usdRate: this.usdRate
    }).subscribe(() => {
      this._toasterService.success(this._localizationService.instant('::SuccessToast', this._localizationService.instant('::Currency:AddCurrency')));
      this.visible = false;
      this.loadCurrencies();
    });
  }

  openAddModal() {
    this.clear();
    this.visible = true;
  }
}
