import { Component, OnInit } from '@angular/core';
import { BinCardService } from '@proxy/bins';
import { CreateBinDtoInput, GetBinDtoInput } from '@proxy/bins/dtos';
import { faker } from '@faker-js/faker';
import { CardsService, Supplier } from '@proxy/cards';
import { CreateCardInput } from '@proxy/cards/dto';
import { LocalizationService, PermissionService } from '@abp/ng.core';
import { CurrencyService } from '@proxy/currencies';
import { CurrencyDto } from '@proxy/currencies/dto';
import { ToasterService } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-bins',
  templateUrl: './bins.component.html',
  styleUrl: './bins.component.scss'
})
export class BinsComponent implements OnInit {
  canCreate: boolean;

  layout: string = 'grid';
  bins!: any[];
  currencies!: CurrencyDto[];
  visible: boolean = false;
  addBinVisible: boolean = false;
  firstName: string;
  lastName: string;
  amount: number;
  note: string;

  // Add bin
  binName: string;
  binDescription: string;
  binSupplier: { name: string, code: number };
  binCreationFixedFee: number;
  binCreationPercentFee: number;
  binFundingFixedFee: number;
  binFundingPercentFee: number;
  binCurrencyId: CurrencyDto;
  binMapping: string;

  textSearch: string;
  suppliers: any[];
  selectedBin: string;
  constructor(
    private _cardService: CardsService,
    private _permissionService: PermissionService,
    private _binCardService: BinCardService,
    private _currencyService: CurrencyService,
    private _toasterService: ToasterService,
    private _localizationService: LocalizationService
  ) {
    this.canCreate = this._permissionService.getGrantedPolicy('Bin.Add');
    this.suppliers = [
      { name: "Vmcardio", code: Supplier.Vmcardio },
      { name: "Vcc51", code: Supplier.Vcc51}
    ];
  }

  ngOnInit(): void {
    this.loadBinList();
    this.loadCurrencyList();
  }
  
  loadCurrencyList() {
    this._currencyService.getList().subscribe((res) => {
      this.currencies = res;
    });
  }

  loadBinList() {
    var input: GetBinDtoInput = {
      filter: "",
      skipCount: 0,
      maxResultCount: 10,
    }
    this._binCardService.getList(input).subscribe((res) => {
      this.bins = res;
    })
  }
  
  pageChange(e) {
    console.log(e)
  }

  onCreateCardClicked(e, binId) {
    this.selectedBin = binId;
    this.visible = true;
  }

  randomData() {
    this.firstName = faker.person.firstName();
    this.lastName = faker.person.lastName();
  }

  createCard() {
    let payload: CreateCardInput = {
      amount: this.amount,
      binId: this.selectedBin,
      cardName: `${this.firstName} ${this.lastName}`
    };

    this._cardService.create(payload).subscribe(() => {
      console.log("OK");
    });
  }

  openCreateBinModal() {
    this.binName = null;
    this.binDescription = null;
    this.binSupplier = null;
    this.binCreationFixedFee = null;
    this.binCreationPercentFee = null;
    this.binFundingFixedFee = null;
    this.binFundingPercentFee = null;
    this.binCurrencyId = null;
    this.binMapping = null;
    this.addBinVisible = true;
  }

  createBin() {
    let payload: CreateBinDtoInput = {
      name: this.binName,
      description: this.binDescription,
      supplier: this.binSupplier?.code,
      currencyId: this.binCurrencyId?.id,
      supplierMapping: this.binMapping,
      creationFixedFee: this.binCreationFixedFee,
      creationPercentFee: this.binCreationPercentFee,
      fundingFixedFee: this.binFundingFixedFee,
      fundingPercentFee: this.binFundingPercentFee
    }
    this._binCardService.create(payload).subscribe((res) => {
      this.loadBinList();
      this.addBinVisible = false;
      if (res.id) {
        this._toasterService.success(this._localizationService.instant('::SuccessToast', this._localizationService.instant('::Bin:AddBin')));
      } else {
        //

      }
    });
  }

  deleteBin(id: string) {
    this._binCardService.delete(id).subscribe(res => 
      this._toasterService.success(this._localizationService.instant('::SuccessToast', this._localizationService.instant('::Bin:DeleteBin'))));
      this.loadBinList();
  }
}
