import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ApiService } from '../../services/api.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  providers: [DatePipe],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  bookings: any[] = [];
  loading = true;
  user: any = null;

  constructor(private apiService: ApiService, private authService: AuthService) {
    this.loadUser();
  }

  loadUser() {
    this.user = this.authService.currentUserValue;
  }

  addCredits() {
    const amount = 500; // Fixed top-up for demo
    this.apiService.topUp(amount).subscribe({
      next: (res: any) => {
        // Update local session with new balance
        this.authService.setSession(res.data);
        this.user = res.data;
        alert(`Successfully added ₹${amount} to your wallet!`);
      },
      error: (err) => {
        alert('Failed to top up balance');
      }
    });
  }

  ngOnInit() {
    this.loadBookings();
  }

  loadBookings() {
    this.apiService.getMyBookings().subscribe({
      next: (res: any) => {
        this.bookings = res.data;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
      }
    });
  }
}
