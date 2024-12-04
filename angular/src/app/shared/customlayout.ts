import { DEFAULT_DYNAMIC_LAYOUTS, ReplaceableComponentsService } from "@abp/ng.core";
import { APP_INITIALIZER, inject } from "@angular/core";
import { NavItemsComponent } from "./navitem/nav-item/nav-item.component";

export const eCustomLayout = {
  key: "NavItemLayout",
  component: "NavItemComponent",
};

export const CUSTOM_LAYOUT_PROVIDERS = [
  {
    provide: APP_INITIALIZER,
    useFactory: configureLayoutFn,
    deps: [ReplaceableComponentsService],
    multi: true,
  },
];
function configureLayoutFn() {
  const service = inject(ReplaceableComponentsService);
  return () => {
    service.add({
      key: eCustomLayout.component,
      component: NavItemsComponent,
    });
  };
}

export const myDynamicLayouts = new Map<string, string>([...DEFAULT_DYNAMIC_LAYOUTS, [eCustomLayout.key, eCustomLayout.component]]);
