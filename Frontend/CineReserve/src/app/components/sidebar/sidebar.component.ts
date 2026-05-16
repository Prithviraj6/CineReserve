import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { SidebarService } from '../../services/sidebar.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  user$: Observable<any>;

  constructor(
    public authService: AuthService, 
    private router: Router,
    public sidebarService: SidebarService
  ) {
    this.user$ = this.authService.currentUser$;
  }

  toggleSidebar() {
    this.sidebarService.toggle();
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
