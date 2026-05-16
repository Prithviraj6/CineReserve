import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  email = '';
  password = '';
  loading = false;
  error = '';
  returnUrl: string = '/movies';
  showPassword = false;

  constructor(
    private apiService: ApiService, 
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/movies';
  }

  onSubmit() {
    this.loading = true;
    this.error = '';
    
    this.apiService.login({ email: this.email, password: this.password }).subscribe({
      next: (res: any) => {
        const userData = res.data || res;
        this.authService.setSession(userData);
        
        // If we have a specific returnUrl (from AuthGuard), go there.
        // Otherwise, admins go to /admin and users go to /movies.
        if (this.returnUrl !== '/movies') {
          this.router.navigateByUrl(this.returnUrl);
        } else if (userData.role === 'Admin') {
          this.router.navigate(['/admin']);
        } else {
          this.router.navigate(['/movies']);
        }
      },
      error: (err) => {
        this.error = err.error?.message || 'Login failed';
        this.loading = false;
      }
    });
  }
}
