import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { Product } from 'src/modules/global-shared/interfaces/IProduct';
import { Brand } from 'src/modules/global-shared/interfaces/IBrands';
import { Type } from 'src/modules/global-shared/interfaces/IType';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];
  brnadIdSelected = 0;
  typeIdSelected = 0;
  sortOptions = [
    { name: 'Alphabetical', value: 'Name' },
    { name: 'Price: Low to High', value: 'PriceAsc' },
    { name: 'Price: High to low', value: 'PriceDesc' },
  ];
  sortSelected = 'name';
  disabled = false;

  constructor(private shopservice: ShopService) {}
  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }
  getProducts() {
    this.shopservice
      .getProducts(this.brnadIdSelected, this.typeIdSelected, this.sortSelected)
      .subscribe({
        next: (response) => {
          (this.products = response.data),
            console.log('products', this.products);
        },
        error: (error) => console.log(error),
      });
  }
  getBrands() {
    this.shopservice.getBrnads().subscribe({
      next: (response) => (this.brands = [{ id: 0, name: 'All' }, ...response]),
      error: (error) => console.log(error),
    });
  }
  getTypes() {
    this.shopservice.getTypes().subscribe({
      next: (response) => (this.types = [{ id: 0, name: 'All' }, ...response]),
      error: (error) => console.log(error),
    });
  }
  onBrandSelected(brnadId: number) {
    this.brnadIdSelected = brnadId;
    this.getProducts();
  }
  onTypeSelected(typeId: number) {
    this.typeIdSelected = typeId;
    this.getProducts();
  }
  onSortSelected(event: any) {
    this.sortSelected = event.target.value;
    this.getProducts();
  }
}
