import { LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { th } from '@faker-js/faker/.';
import { GetCardInput } from '@proxy/cards/dto';
import { CurrencyDto } from '@proxy/currencies/dto';
import { DepositTransactionStatus, GatewayType, PaymentsService } from '@proxy/payments';
import { DepositTransactionDto, GetDepositTransactionInput, GetPaymentMethodsInput, PaymentMethodDto } from '@proxy/payments/dtos';
import * as crypto from 'crypto-js';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss'],
})
export class PaymentComponent implements OnInit {
  input: GetCardInput;
  loading: boolean = true;
  gateways!: PaymentMethodDto[];
  transactions!: DepositTransactionDto[];
  items: MenuItem[] | undefined;
  activeItem: MenuItem | undefined;


  visible: boolean = false;
  selectedGateway: PaymentMethodDto;
  inputAmount: number;

  secretKey = Date.now().toString();
  dataViewIdName: string = 'data-view';
  constructor(
    private _paymentService: PaymentsService, 
    private _toasterService: ToasterService,
    private _localizationService: LocalizationService
  ) {}

  ngOnInit() {
    this.items = [
      { label: 'Gateways', icon: 'pi pi-home' },
      { label: 'Transactions', icon: 'pi pi-chart-line' }
    ];
    this.activeItem = this.items[0];
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

  loadTransactions() {
    this.loading = true;
    let payload: GetDepositTransactionInput = {
      filter: '',
      skipCount: 0,
      maxResultCount: 100,
    }
    this._paymentService.getDepositTransactionsByInput(payload).subscribe((res) => {
      this.loading = false;
      this.transactions = res.items;
    });
  }

  edit(c: CurrencyDto) {
    this.visible = true;
  }

  openAddModal(gateway: PaymentMethodDto) {
    this.visible = true;
    this.selectedGateway = gateway;
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

  getTransactionType(transaction: DepositTransactionDto) {
    switch (transaction.transactionStatus) {
      case DepositTransactionStatus.Pending:
        return 'info';
      case DepositTransactionStatus.Completed:
        return 'success';
      case DepositTransactionStatus.Failed:
        return 'warning';
    }
  }

  getTransactionStatusType(status: DepositTransactionStatus) {
    return this._localizationService.instant(`::DepositTransactionStatusType_${status}`);
  }


  submitDeposit() {
    const hmacDigest = crypto.HmacSHA1(this.secretKey, this.selectedGateway.id.toString());

    this._paymentService.createTransactionByIdAndInput(this.selectedGateway.id, {
      signature: hmacDigest.toString(),
      amount: this.inputAmount
    }).subscribe((res) => {
      this._toasterService.success('Deposit is successful');
      this.visible = false;
    });
  }

  onActiveItemChange(event: MenuItem) {
    this.activeItem = event;
    if (event == this.items[0]) {
      this.loadGateways();
    }
    if (event == this.items[1]) {
      this.loadTransactions();
    }
  }
}
