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

  items = [
    {
        label: 'File',
        icon: 'pi pi-file',
        items: [
            {
                label: 'New',
                icon: 'pi pi-plus',
                items: [
                    {
                        label: 'Document',
                        icon: 'pi pi-file'
                    },
                    {
                        label: 'Image',
                        icon: 'pi pi-image'
                    },
                    {
                        label: 'Video',
                        icon: 'pi pi-video'
                    }
                ]
            },
            {
                label: 'Open',
                icon: 'pi pi-folder-open'
            },
            {
                label: 'Print',
                icon: 'pi pi-print'
            }
        ]
    },
    {
        label: 'Edit',
        icon: 'pi pi-file-edit',
        items: [
            {
                label: 'Copy',
                icon: 'pi pi-copy'
            },
            {
                label: 'Delete',
                icon: 'pi pi-times'
            }
        ]
    },
    {
        label: 'Search',
        icon: 'pi pi-search'
    },
    {
        separator: true
    },
    {
        label: 'Share',
        icon: 'pi pi-share-alt',
        items: [
            {
                label: 'Slack',
                icon: 'pi pi-slack'
            },
            {
                label: 'Whatsapp',
                icon: 'pi pi-whatsapp'
            }
        ]
    }
];

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

}
