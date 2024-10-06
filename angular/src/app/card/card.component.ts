import { AuthService } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { CardsService } from '@proxy/cards';
import { GetCardInput } from '@proxy/cards/dto';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
})
export class CardComponent implements OnInit {
    products!: any[];
    input: GetCardInput;
  get hasLoggedIn(): boolean {
    return this.authService.isAuthenticated
  }

  constructor(private authService: AuthService, private cardService: CardsService) {}

  ngOnInit() {
    this.cardService.getList(this.input).subscribe((data) => {
        this.products = data;
    });
  }

  login() {
    this.authService.navigateToLogin();
  }
}
