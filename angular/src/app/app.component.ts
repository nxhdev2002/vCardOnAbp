import { ReplaceableComponentsService } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { NavItemsComponent } from './shared/navitem/nav-item/nav-item.component';
import { eThemeLeptonXComponents } from '@abp/ng.theme.lepton-x'; // imported eThemeBasicComponents

@Component({
  selector: 'app-root',
  template: `
    <abp-loader-bar></abp-loader-bar>
    <abp-dynamic-layout></abp-dynamic-layout>
  `,
})
export class AppComponent implements OnInit {
  constructor(private replaceableComponents: ReplaceableComponentsService) {} // injected ReplaceableComponentsService

  ngOnInit(): void {
    this.replaceableComponents.add({
      component: NavItemsComponent,
      key: eThemeLeptonXComponents.NavItems,
    });
  }
}
