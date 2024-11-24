import { LocalizationService, PermissionService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { GetCardInput } from '@proxy/cards/dto';
import { CurrencyDto } from '@proxy/currencies/dto';
import { DepositTransactionStatus, GatewayType, PaymentsService } from '@proxy/payments';
import { DepositTransactionDto, GetDepositTransactionInput, GetPaymentMethodsInput, PaymentMethodDto } from '@proxy/payments/dtos';
import * as crypto from 'crypto-js';
import { MenuItem, ConfirmationService } from 'primeng/api';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss'],
  providers: [ConfirmationService]
})
export class PaymentComponent implements OnInit {
  input: GetCardInput;
  loading: boolean = true;
  gateways!: PaymentMethodDto[];
  // User transactions
  transactions!: DepositTransactionDto[];
  statuses: any[];
  selectedStatus: { name: string, code: number }[];
  // Admin transactions
  allTransactions!: DepositTransactionDto[];
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
    private _localizationService: LocalizationService,
    private _permissionService: PermissionService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit() {
    this.items = [
      { label: this._localizationService.instant('::Gateway'), icon: 'pi pi-home', visible: this._permissionService.getGrantedPolicy('Payment.View') },
      { label: this._localizationService.instant('::Transaction'), icon: 'pi pi-chart-line', visible: this._permissionService.getGrantedPolicy('Payment.ViewDepositTransaction') },
      { label: this._localizationService.instant('::PendingApproval'), icon: 'pi pi-clock', visible: this._permissionService.getGrantedPolicy('Payment.ApproveTransaction') }
    ];

    this.activeItem = this.items[0];
    this.loadStatuses();
    this.loadGateways();
  }

  loadStatuses() {
    this.statuses = [];
    for(let status in DepositTransactionStatus) {
      if (typeof DepositTransactionStatus[status] === 'number') {
        this.statuses.push({name: this._localizationService.instant("::DepositTransactionStatusType_" + DepositTransactionStatus[status]), code: DepositTransactionStatus[status]});
      }
    }
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
      status: this.selectedStatus?.map(s => s.code),
      filter: '',
      skipCount: 0,
      maxResultCount: 100,
    }
    this._paymentService.getDepositTransactionsByInput(payload).subscribe((res) => {
      setTimeout(() => {
        this.loading = false;
        this.transactions = res.items;
      }, 500);
    });
  }

  loadAllTransactions() {
    this.loading = true;
    let payload: GetDepositTransactionInput = {
      status: this.selectedStatus?.map(s => s.code),
      filter: '',
      skipCount: 0,
      maxResultCount: 100,
    }
    this._paymentService.getPendingTransactionsByInput(payload).subscribe((res) => {
      setTimeout(() => {
        this.loading = false;
        this.allTransactions = res.items;
      }, 500);
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
    if (event == this.items[2]) {
      this.loadAllTransactions();
    }
  }


  approveConfirm(event: Event, id: string) {
    console.log(event)
    this.confirmationService.confirm({
        target: event.target as EventTarget,
        message: 'Are you sure you want to proceed?',
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
            this.processTransaction(id);
        }
    });
  }

  rejectConfirm(event: Event, id: string) {
      this.confirmationService.confirm({
          target: event.target as EventTarget,
          message: 'Do you want to delete this record?',
          icon: 'pi pi-info-circle',
          acceptButtonStyleClass: 'p-button-danger p-button-sm',
          accept: () => {
            this.processTransaction(id, false);
          }
      });
  }

  isAllowAction(transaction: DepositTransactionDto) {
    return transaction?.transactionStatus == DepositTransactionStatus.Pending;
  }

  processTransaction(id: string, isApprove: boolean = true) {
    var request = isApprove ? this._paymentService.approveTransactionById(id) : this._paymentService.rejectTransactionById(id);

    request.subscribe((res) => {
      if (res.success) {
        this._toasterService.success(res.message);
        this.loadAllTransactions();
      }
    })
  }

}
