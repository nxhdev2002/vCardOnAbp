import { AuthService, LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { CardsService, CardStatus } from '@proxy/cards';
import { CardDto, CardRowAction, GetCardInput } from '@proxy/cards/dto';
import { CardsManagementService } from '@proxy/management/cards';
import { GetCardManagementInput } from '@proxy/management/cards/dto';
import { MenuItem } from 'primeng/api';
import { FundCardModalComponent } from 'src/app/shared/components/fund-cards/fund-card.component';
import { NoteCardModalComponent } from 'src/app/shared/components/note-cards/note-card.component';

@Component({
  selector: 'app-card-management',
  templateUrl: './card-management.component.html',
  styleUrls: ['./card-management.component.css'],
})
export class CardManagementComponent implements OnInit {
  @ViewChild('menu1') menu1: any;
  @ViewChild('fundcard', { static: true }) fundcard: | FundCardModalComponent | undefined;
  @ViewChild('notecard', { static: true }) notecard: | NoteCardModalComponent | undefined;

  input: GetCardInput;
  loading: boolean = true;
  cards!: CardDto[];
  totalRecords: number = 10;
  rowActionEnum = CardRowAction;

  filter: string;
  items: MenuItem[] | undefined;
  dataViewIdName: string = 'data-view';
  constructor(
    private authService: AuthService, 
    private cardManagementService: CardsManagementService,
    private cardService: CardsService,
    private toasterService: ToasterService,
    private localizeService: LocalizationService,
    private router: Router
  ) {}

  ngOnInit() {

  }

  login() {
    this.authService.navigateToLogin();
  }

  clear(table: any) {
    table.clear();
  }

  viewCardDetails(card: CardDto) {
    this.router.navigate([`/card/${card.id}`]);
  }

  onPageChange(e) {
    this.loadCardData(e.first, e.rows);
  }

  loadCardData(skip: number, take: number) {
    this.loading = true;
    let payload: GetCardManagementInput = {
      filter: this.filter,
      skipCount: skip,
      maxResultCount: take,
      suppliers: null,
      binIds: null,
      statuses: null,
      ownerIds: null
    };

    this.cardManagementService.getList(payload).subscribe((res) => {
      this.loading = false;
      this.cards = res.items;
      this.totalRecords = res.totalCount;
    });
  }
  

  getCardStatus(cardStatus: CardStatus) {
    switch (cardStatus) {
      case CardStatus.Active:
        return [this.localizeService.instant('::CardStatus:Active'), 'success'];
      case CardStatus.Inactive:
        return [this.localizeService.instant('::CardStatus:Inactive'), 'danger'];
      case CardStatus.Pending:
        return [this.localizeService.instant('::CardStatus:Pending'), 'info'];
      case CardStatus.Lock:
        return [this.localizeService.instant('::CardStatus:Lock'), 'warning'];
      case CardStatus.PendingDelete:
        return [this.localizeService.instant('::CardStatus:PendingDelete'), 'warning'];
      default:
        return [this.localizeService.instant('::CardStatus:Unknown'), 'contrast'];
    }
  }

  renderRowActions(card: CardDto, event) {
    // check if rowAction include 
    this.items = [];
    let rowActions = [];
    let rowManageActions = [];

    if (card.rowActions.includes(this.rowActionEnum.View)) rowActions.push({ label: this.localizeService.instant('::View'), icon: 'pi pi-eye', command: () => this.viewCard(card.id) });
    if (card.rowActions.includes(this.rowActionEnum.Fund)) rowActions.push({ label: this.localizeService.instant('::Fund'), icon: 'pi pi-wallet', command: () => this.fundcard.show(card) });
    if (card.rowActions.includes(this.rowActionEnum.Delete)) rowActions.push({ label: this.localizeService.instant('::Delete'), icon: 'pi pi-trash', command: () => this.deleteCard(card.id)  });
    if (card.rowActions.includes(this.rowActionEnum.Refresh)) rowActions.push({ label: this.localizeService.instant('::Refresh'), icon: 'pi pi-sync', command: () => this.refreshCard(card.id) });
    if (card.rowActions.includes(this.rowActionEnum.Note)) rowActions.push({ label: this.localizeService.instant('::Note'), icon: 'pi pi-pen-to-square', command: () => this.notecard.show(card) });
    if (card.rowActions.includes(this.rowActionEnum.CancelDelete)) rowActions.push({ label: this.localizeService.instant('::CancelDelete'), icon: 'pi pi-undo', command: () => this.cancelDeleleCard(card.id) });

    if (card.rowActions.includes(this.rowActionEnum.ApproveDelete)) rowManageActions.push({ label: this.localizeService.instant('::CardDeletionApprove'), icon: 'pi pi-check' });
    if (card.rowActions.includes(this.rowActionEnum.RejectDelete)) rowManageActions.push({ label: this.localizeService.instant('::CardDeletionReject'), icon: 'pi pi-times' });


    if (rowActions.length > 0)
      this.items.push({
        label: 'Menu',
        items: rowActions
      });
    
    if (rowManageActions.length > 0)
      this.items.push({
        label: 'Manage',
        items: rowManageActions
    });
    
    this.menu1.show(event);
  }

  isDisableMenu(card: CardDto) {
    return card.rowActions.length === 0;
  }

  refreshCard(id: string) {
    this.cardService.refreshCardById(id).subscribe((res) => {
      if (res.success) this.toasterService.success(res.message);
    });
  }

  viewCard(id: string) {
    this.router.navigate([`/card/${id}`]);
  }

  cancelDeleleCard(id: string) {
    this.cardService.cancelDeleteById(id).subscribe((res) => {
      if (res.success) this.toasterService.success(res.message);
      this.loadCardData(0, 10);
    });
  }

  deleteCard(id: string) {
    this.cardService.delete(id).subscribe(() => {
      this.loadCardData(0, 10);
    });
  }
}
