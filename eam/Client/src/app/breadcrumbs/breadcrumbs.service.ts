import { Injectable } from '@angular/core';
import { BreadCrumbsPair } from '../home/home.models';

@Injectable({
  providedIn: 'root'
})
export class BreadcrumbsService {

  public finalString: string;

  constructor() { }

  updateBreadCrumbs(strings: BreadCrumbsPair[]) {
  }
}
