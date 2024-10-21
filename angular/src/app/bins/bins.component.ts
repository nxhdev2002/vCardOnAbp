import { Component, OnInit } from '@angular/core';
import { BinCardService } from '@proxy/bins';
import { GetBinDtoInput } from '@proxy/bins/dtos';
import { faker } from '@faker-js/faker';

@Component({
  selector: 'app-bins',
  templateUrl: './bins.component.html',
  styleUrl: './bins.component.scss'
})
export class BinsComponent implements OnInit {
  layout: string = 'grid';
  products!: any[];
  visible: boolean = false;
  firstName: string;
  textSearch: string;
  constructor(private binService: BinCardService) {}

  ngOnInit(): void {
    var input: GetBinDtoInput = {
      filter: "",
      skipCount: 0,
      maxResultCount: 10,
    }
    this.binService.getList(input).subscribe((res) => {
      this.products = res;
    })
  }

  pageChange(e) {
    console.log(e)
  }

  onCreateCardClicked(e) {
    this.visible = true;
  }

  randomData() {
    this.firstName = faker.person.firstName();
  }
}
