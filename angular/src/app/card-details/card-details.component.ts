import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CardsService } from '@proxy/cards';
import { CardDto, CardSecretDto, CardTransactionDto, GetCardTransactionInput } from '@proxy/cards/dto';
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
  constructor(private cardService: CardsService, private route: ActivatedRoute, private confirmationService: ConfirmationService) {}

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
}
