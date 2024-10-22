import { AuthService } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { CardsService } from '@proxy/cards';
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
    
  filter: string;
  
  dataViewIdName: string = 'data-view';
  constructor(private authService: AuthService, private cardService: CardsService) {}

  ngOnInit() {
    this.loading = true;
    let payload: GetCardInput = {
      filter: this.filter,
      skipCount: 0,
      maxResultCount: 100
    };

    this.cardService.getList(payload).subscribe((res) => {
      this.loading = false;
      this.cards = res.items;
    });
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
}
