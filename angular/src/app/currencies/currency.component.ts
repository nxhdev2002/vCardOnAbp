import { AuthService } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { GetCardInput } from '@proxy/cards/dto';
import { CurrencyService } from '@proxy/currencies';
import { CurrencyDto } from '@proxy/currencies/dto';

@Component({
  selector: 'app-currency',
  templateUrl: './currency.component.html',
  styleUrls: ['./currency.component.scss'],
})
export class CurrencyComponent implements OnInit {
  input: GetCardInput;
  loading: boolean = true;
  currencies!: CurrencyDto[];
      
  dataViewIdName: string = 'data-view';
  constructor(
    private authService: AuthService, 
    private currency: CurrencyService,
  ) {}

  ngOnInit() {
    this.loading = true;

    this.currency.getList().subscribe((res) => {
      this.loading = false;
      this.currencies = res;
    });
  }

  login() {
    this.authService.navigateToLogin();
  }

  clear(table: any) {
    table.clear();
  }
}
