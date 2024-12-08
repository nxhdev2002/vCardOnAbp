import { ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BinCardService } from '@proxy/bins';
import { BinDto } from '@proxy/bins/dtos';
import { CardsService } from '@proxy/cards';
import { CardDto, CardSecretDto, CardTransactionDto, FundCardInput, GetCardTransactionInput } from '@proxy/cards/dto';
import { ConfirmationService, MessageService } from 'primeng/api';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-card-details',
  templateUrl: './card-details.component.html',
  styleUrls: ['./card-details.component.scss'],
  providers: [ConfirmationService, MessageService],
})
export class CardDetailsComponent implements OnInit {
  loading: boolean = true;
  transactions: CardTransactionDto[];
  filter: string;
  
  cardId: string;
  card: CardDto;
  secret: CardSecretDto;
  isViewing: boolean = false;

  fundDialogVisible: boolean = false;
  fundingFee: number = 0;
  fundingAmount: number;
  fundingBin: BinDto;

  constructor(
    private cardService: CardsService, 
    private route: ActivatedRoute, 
    private confirmationService: ConfirmationService,
    private binService: BinCardService,
    private toasterService: ToasterService
  ) {}

  ngOnInit() {
    this.loading = true;
    let payload: GetCardTransactionInput = {
      filter: this.filter,
      skipCount: 0,
      maxResultCount: 100
    };
    this.cardId = this.route.snapshot.paramMap.get('id');
    

    const transactions = this.cardService.getTransaction(this.cardId, payload);
    const cardDetail = this.cardService.get(this.cardId);

    forkJoin([transactions, cardDetail]).subscribe((res) => {
      this.loading = false;
      this.transactions = res[0]?.items;
      this.card = res[1];
    })
  }

  clear(table: any) {
    table.clear();
  }

  viewCardSecret() {
    if (!this.secret)
      this.cardService.getSecret(this.cardId).subscribe((res) => {
        this.secret = res;
        this.isViewing = true;
        document.getElementById("exp").innerHTML = this.secret.expirationTime;
        document.getElementById("cvv").innerHTML = this.secret.cvv;
      });
    else {
      this.isViewing = true;
      document.getElementById("exp").innerHTML = this.secret.expirationTime;
      document.getElementById("cvv").innerHTML = this.secret.cvv;
    }
  }

  hideCardSecret() {
    this.isViewing = false;
    document.getElementById("exp").innerHTML = "**/**";
    document.getElementById("cvv").innerHTML = "***";
  }

  deleteCard(event: Event) {
      this.confirmationService.confirm({
          target: event.target as EventTarget,
          message: 'Are you sure that you want to proceed?',
          header: 'Confirmation',
          icon: 'pi pi-exclamation-triangle',
          acceptIcon:"none",
          rejectIcon:"none",
          rejectButtonStyleClass:"p-button-text",
          accept: () => {
            this.loading = true;
            this.cardService.delete(this.cardId).subscribe(() => {
              this.loading = false;
              window.history.back();
            });
          }
      });
  }

  openFundCard() {
    this.fundDialogVisible = true;
    if (!this.fundingBin) {
      this.loading = true;
      this.binService.get(this.card.binId).subscribe((res) => {
        if (res) this.fundingBin = res;
        this.loading = false;
      });
    }
      
  }

  calculateFee() {
    this.fundingFee = this.fundingAmount + (this.fundingAmount * this.fundingBin?.fundingPercentFee / 100 + this.fundingBin?.fundingFixedFee);
  }
  
  fundCard() {
    this.loading = true;
    let payload: FundCardInput = {
      amount: this.fundingAmount,
    }
    this.cardService.fund(this.cardId, payload).subscribe((res) => {
      this.loading = false;
      if (res.success) {
        this.fundDialogVisible = false;
        this.toasterService.success(res.message);
      } else {
        this.toasterService.error(res.message);
      }
    });
  }
}
