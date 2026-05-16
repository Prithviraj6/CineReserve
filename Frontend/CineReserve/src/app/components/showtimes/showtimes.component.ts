import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { ShowTime } from '../../models/api.models';

@Component({
  selector: 'app-showtimes',
  standalone: true,
  imports: [CommonModule],
  providers: [DatePipe],
  templateUrl: './showtimes.component.html',
  styleUrl: './showtimes.component.css'
})
export class ShowtimesComponent implements OnInit {
  showtimes: ShowTime[] = [];
  loading = true;
  movieId: number = 0;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private apiService: ApiService
  ) {}

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.movieId = +params['movieId'];
      this.loadShowtimes();
    });
  }

  loadShowtimes() {
    this.apiService.getShowtimes(this.movieId).subscribe({
      next: (res: any) => {
        this.showtimes = res.data;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
      }
    });
  }

  bookSeats(showtimeId: number) {
    this.router.navigate(['/seats', showtimeId]);
  }

  goBack() {
    this.router.navigate(['/']);
  }
}
