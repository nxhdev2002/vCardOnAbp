import { AuthService } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CardsService, CardStatus } from '@proxy/cards';
import { CardDto, GetCardInput } from '@proxy/cards/dto';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
})
export class CardComponent implements OnInit {
  input: GetCardInput;
  loading: boolean = true;
  cards!: CardDto[];
  totalRecords: number = 10;

  filter: string;
  
  dataViewIdName: string = 'data-view';
  constructor(
    private authService: AuthService, 
    private cardService: CardsService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadCardData(0, 10);
  }

  login() {
    this.authService.navigateToLogin();
  }

  clear(table: any) {
    table.clear();
  }

  viewCardSecret(card: CardDto) {
    let exp = document.getElementById(`${card.id}-exp`);
    let cvv = document.getElementById(`${card.id}-cvv`);

    if (exp.getAttribute(this.dataViewIdName) === 'false') {
        this.cardService.getSecret(card.id).subscribe((res) => {
        
            exp.innerText = res.expirationTime;
            cvv.innerText = res.cvv;
    
            exp.setAttribute(this.dataViewIdName, 'true');
            cvv.setAttribute(this.dataViewIdName, 'true');
        });
    } else {
        exp.innerText = "** / **";
        cvv.innerText = "***";

        exp.setAttribute(this.dataViewIdName, 'false');
        cvv.setAttribute(this.dataViewIdName, 'false');
    }
  }

  viewCardDetails(card: CardDto) {
    this.router.navigate([`/card/${card.id}`]);
  }

  onPageChange(e) {
    this.loadCardData(e.first, e.rows);
  }

  loadCardData(skip: number, take: number) {
    this.loading = true;
    let payload: GetCardInput = {
      filter: this.filter,
      skipCount: skip,
      maxResultCount: take
    };

    this.cardService.getList(payload).subscribe((res) => {
      this.loading = false;
      this.cards = res.items;
      this.totalRecords = res.totalCount;
    });
  }
  
  isAllowViewDetail(card: CardDto) {
    return card.cardStatus == CardStatus.Active;
  }

  isAllowViewSecret(card: CardDto) {
    return card.cardStatus == CardStatus.Active;
  }

  getCardStatus(cardStatus: CardStatus) {
    switch (cardStatus) {
      case CardStatus.Active:
        return ['Active', 'success'];
      case CardStatus.Inactive:
        return ['Inactive', 'danger'];
      case CardStatus.Pending:
        return ['Pending', 'info'];
      case CardStatus.Lock:
        return ['Lock', 'warning'];
      case CardStatus.PendingDelete:
        return ['Pending Delete', 'warning'];
      default:
        return ['Unknown', 'contrast'];
    }
  }
}
