import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShopRoutingModule } from './shop-routing.module';
import { ShopComponent } from './shop/shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { GlobalSharedModule } from '../global-shared/global-shared.module';

@NgModule({
  declarations: [ShopComponent, ProductItemComponent],
  imports: [CommonModule, ShopRoutingModule, GlobalSharedModule],
  exports: [ShopComponent],
})
export class ShopModule {}
