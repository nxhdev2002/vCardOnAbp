import { Component, OnInit } from '@angular/core';
import { GetCardInput } from '@proxy/cards/dto';
import { AccountsService } from '@proxy/controllers';
import { DatePipe } from '@angular/common';
import { UserTransactionType } from '@proxy/transactions';
import { GetUserTransactionInput, UserTransactionDto } from '@proxy/accounts/dtos';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.scss'],
  providers: [DatePipe],
})
export class TransactionComponent implements OnInit {
  input: GetCardInput;
  loading: boolean = true;
  transactions!: UserTransactionDto[];
      
  visible: boolean = false;
  constructor(
    private _accountService: AccountsService
  ) {}

  ngOnInit() {
    this.getTransactions();
  }

  getTransactions() {
    this.loading = true;
    let payload: GetUserTransactionInput = {
      filter: '',
      skipCount: 0,
      maxResultCount: 100,
    }

    this._accountService.getTransactionsByInput(payload).subscribe((res) => {
      this.loading = false;
      this.transactions = res.items;
    });
  }

  getTransactionType(type: UserTransactionType) {
    switch (type) {
      case UserTransactionType.FundCard:
        return 'Fund Card';
      case UserTransactionType.CreateCard:
        return 'Create Card';
      case UserTransactionType.Deposit:
        return 'Deposit';
      default:
        return 'Unknown';
    }
  }
}
