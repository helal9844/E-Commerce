import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../global-shared/interfaces/IPaging';
import { Product } from '../global-shared/interfaces/IProduct';
import { environment } from 'src/environments/environment';
import { Brand } from '../global-shared/interfaces/IBrands';
import { Type } from '../global-shared/interfaces/IType';
@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseURL = environment.baseURL;
  constructor(private http: HttpClient) {}
  getProducts(brandId?: number, typeId?: number, sort?: string) {
    let params = new HttpParams();
    params.append('PageIndex', 1);
    params.append('PageSize', 10);
    if (brandId) params = params.append('BrandId', brandId);
    if (typeId) params = params.append('TypeId', typeId);
    if (sort) params = params.append('Sort', sort);
    return this.http.get<Pagination<Product[]>>(this.baseURL + `Product`, {
      params: params,
    });
  }
  getBrnads() {
    return this.http.get<Brand[]>(`${this.baseURL}Product/brands`);
  }
  getTypes() {
    return this.http.get<Type[]>(`${this.baseURL}Product/types`);
  }
}
