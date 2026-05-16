import { Component } from '@angular/core';
import { RouterOutlet, Router, NavigationEnd } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { filter } from 'rxjs/operators';

import { SidebarService } from './services/sidebar.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SidebarComponent, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  title = 'CineReserveFrontend';
  hideSidebar = false;

  constructor(private router: Router, public sidebarService: SidebarService) {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: any) => {
      const url = event.urlAfterRedirects || event.url;
      // Remove query params and hash fragments
      const cleanUrl = url.split('?')[0].split('#')[0];
      this.hideSidebar = ['/', '/login', '/register'].includes(cleanUrl);
    });
  }
}
