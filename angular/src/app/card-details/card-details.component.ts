import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CardsService } from '@proxy/cards';
import { CardDto, CardTransactionDto, GetCardTransactionInput } from '@proxy/cards/dto';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-card-details',
  templateUrl: './card-details.component.html',
  styleUrls: ['./card-details.component.scss'],
})
export class CardDetailsComponent implements OnInit {
  loading: boolean = true;
  transactions: CardTransactionDto[];
  filter: string;
  
  cardId: string;
  card: CardDto;
  constructor(private cardService: CardsService, private route: ActivatedRoute) {}

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
}
