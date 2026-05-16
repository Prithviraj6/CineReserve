import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { Seat } from '../../models/api.models';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-theater-layout',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './theater-layout.component.html',
  styleUrl: './theater-layout.component.css'
})
export class TheaterLayoutComponent implements OnInit {
  seats: Seat[] = [];
  rows: string[] = [];
  loading = true;
  showtimeId = 0;
  showtime: any = null;
  
  selectedSeats: any[] = [];
  bookingInProgress = false;
  error = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private apiService: ApiService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.showtimeId = +params['showtimeId'];
      this.loadSeats();
      this.loadShowtimeDetails();
    });
  }

  loadShowtimeDetails() {
    // We can get showtime info from the seats response if the backend includes it,
    // or just fetch it separately. Since we don't have a getShowtimeById yet, 
    // let's just use the first seat's metadata if available, or just mock it for now.
  }

  loadSeats() {
    this.apiService.getSeats(this.showtimeId).subscribe({
      next: (res: any) => {
        this.seats = res.data;
        if (this.seats.length > 0) {
          this.showtime = {
            movieTitle: this.seats[0].movieTitle,
            theaterHallName: this.seats[0].theaterHallName
          };
        }
        // Extract unique row labels and sort them
        this.rows = [...new Set(this.seats.map(s => s.rowLabel))].sort();
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
      }
    });
  }

  getSeatsInRow(rowLabel: string): Seat[] {
    return this.seats.filter(s => s.rowLabel === rowLabel).sort((a, b) => a.seatNumber - b.seatNumber);
  }

  isSelected(seatId: number): boolean {
    return this.selectedSeats.some(s => s.seatId === seatId);
  }

  toggleSeat(seat: Seat) {
    if (!seat.isAvailable) return;
    
    const index = this.selectedSeats.findIndex(s => s.seatId === seat.seatId);
    if (index > -1) {
      this.selectedSeats.splice(index, 1);
    } else {
      if (this.selectedSeats.length >= 10) {
        this.error = 'Maximum 10 seats allowed per booking.';
        return;
      }
      this.selectedSeats.push(seat);
    }
  }

  getTotalPrice(): number {
    return this.selectedSeats.reduce((sum, seat) => sum + seat.price, 0);
  }

  confirmBooking() {
    if (this.selectedSeats.length === 0) return;

    if (!this.authService.currentUserValue) {
      this.router.navigate(['/login']);
      return;
    }

    this.bookingInProgress = true;
    this.error = '';

    const payload = {
      showtimeId: this.showtimeId,
      seats: this.selectedSeats.map(s => ({ seatId: s.seatId }))
    };

    this.apiService.createBooking(payload).subscribe({
      next: (res: any) => {
        alert('Booking confirmed! Ref: ' + res.data.bookingReference);
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        this.error = err.error?.message || 'Booking failed. Please try again.';
        this.bookingInProgress = false;
        this.loadSeats();
        this.selectedSeats = [];
      }
    });
  }

  goBack() {
    window.history.back();
  }
}
