import { ToasterService } from "@abp/ng.theme.shared";
import { Component, ViewChild, ElementRef } from "@angular/core";
import { BinCardService } from "@proxy/bins";
import { BinDto } from "@proxy/bins/dtos";
import { CardsService } from "@proxy/cards";
import { CardDto, FundCardInput } from "@proxy/cards/dto";

@Component({
    selector: 'fund-card',
    templateUrl: './fund-card.component.html',
})
export class FundCardModalComponent {
    @ViewChild('fundcard', { static: true }) modal: ElementRef | undefined;

    visible: boolean = false;
    loading: boolean = false;
    fundingFee: number;
    fundingAmount: number;
    
    bin: BinDto | undefined;
    card: CardDto | undefined;
    constructor(
      private _toastrService: ToasterService,
      private _binCardService: BinCardService,
      private _cardService: CardsService
    ) {}

    show(card: CardDto): void {
        this.card = card;
        this.visible = true;
        this.getBin(card.binId);
    }

    getBin(binId) {
        this.loading = true;
        this._binCardService.get(binId).subscribe(res => {
            this.loading = false;
            this.bin = res;
        });
    }

    calculateFee() {
      this.fundingFee = this.fundingAmount + (this.fundingAmount * this.bin?.fundingPercentFee / 100 + this.bin?.fundingFixedFee);
    }
    
    fundCard() {
      this.loading = true;
      let payload: FundCardInput = {
        amount: this.fundingAmount,
      }
      this._cardService.fund(this.card.id, payload).subscribe((res) => {
        this.loading = false;
        if (res.success) {
          this.visible = false;
          this._toastrService.success(res.message);
        } else {
          this._toastrService.error(res.message);
        }
      });
    }
}
