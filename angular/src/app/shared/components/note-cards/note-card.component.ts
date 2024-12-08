import { ToasterService } from "@abp/ng.theme.shared";
import { Component, ViewChild, ElementRef } from "@angular/core";
import { CardsService } from "@proxy/cards";
import { CardDto } from "@proxy/cards/dto";

@Component({
    selector: 'note-card',
    templateUrl: './note-card.component.html',
})
export class NoteCardModalComponent {
    @ViewChild('notecard', { static: true }) modal: ElementRef | undefined;

    visible: boolean = false;
    loading: boolean = false;
    noteValue: string;

    card: CardDto | undefined;
    constructor(
      private _cardService: CardsService,
      private _toasterService: ToasterService
    ) {}

    show(card: CardDto): void {
        this.noteValue = null;
        this.card = card;
        this.visible = true;
    }

    save(): void {
        this._cardService.noteByIdAndInput(this.card.id, { value: this.noteValue }).subscribe((res) => {
            if (res.success) {
                this.visible = false;
                this._toasterService.success(res.message);
            } else {
                this._toasterService.error(res.message);
            }
        });
    }
}
