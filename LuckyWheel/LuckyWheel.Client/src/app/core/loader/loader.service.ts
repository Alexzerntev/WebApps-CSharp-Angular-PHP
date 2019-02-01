import { Injectable } from '@angular/core';

@Injectable()
export class LoaderService {

  private loading = false;

  constructor() { }

  public show(delay: number = 0): void {
    setTimeout(() => {
      this.loading = true;
    }, delay)
  }

  public hide(delay: number = 0): void {
    setTimeout(() => {
      this.loading = false;
    }, delay)
  }

}
