import { AuthService, LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { GetCardInput } from '@proxy/cards/dto';
import { CurrencyService } from '@proxy/currencies';
import { CurrencyDto } from '@proxy/currencies/dto';
import { GatewayType, PaymentsService } from '@proxy/payments';
import { GetPaymentMethodsInput, PaymentMethodDto } from '@proxy/payments/dtos';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss'],
})
export class PaymentComponent implements OnInit {
  input: GetCardInput;
  loading: boolean = true;
  gateways!: PaymentMethodDto[];
      
  visible: boolean = false;


  dataViewIdName: string = 'data-view';
  constructor(
    private _paymentService: PaymentsService, 
    private _toasterService: ToasterService,
    private _localizationService: LocalizationService
  ) {}

  ngOnInit() {
    this.loadGateways();
  }

  loadGateways() {
    this.loading = true;
    let payload: GetPaymentMethodsInput = {
      filter: '',
      skipCount: 0,
      maxResultCount: 100,
    }
    this._paymentService.getPaymentMethods(payload).subscribe((res) => {
      this.loading = false;
      this.gateways = res.items;
    });
  }

  edit(c: CurrencyDto) {
    this.visible = true;
  }

  openAddModal() {
    this.visible = true;
  }

  getType(gateways: PaymentMethodDto) {
    switch (gateways.gatewayType) {
      case GatewayType.MANUAL:
        return 'info';
      case GatewayType.AUTOBANK:
        return 'info';
    }
  }

  getGatewayType(gateways: PaymentMethodDto) {
    return this._localizationService.instant(`::GatewayType_${gateways.gatewayType}`);
  }
}
