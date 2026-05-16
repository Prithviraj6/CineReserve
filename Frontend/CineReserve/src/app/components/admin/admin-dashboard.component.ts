import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { Movie } from '../../models/api.models';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.css'
})
export class AdminDashboardComponent implements OnInit {
  movies: Movie[] = [];
  movieShowtimes: any[] = [];
  loading = false;
  showAddModal = false;
  showTimeModal = false;
  selectedMovie: any = null;

  newMovie = {
    title: '',
    description: '',
    genre: '',
    durationMinutes: 120,
    language: 'English',
    posterUrl: '',
    releaseDate: new Date().toISOString().split('T')[0]
  };

  newShowtime = {
    movieId: 0,
    theaterHallId: 1,
    startTimeUtc: '',
    basePrice: 250
  };

  newShowDate = '';
  newShowTime = '';

  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.fetchMovies();
  }

  fetchMovies() {
    this.loading = true;
    this.apiService.getMovies().subscribe({
      next: (res) => {
        this.movies = res.data;
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }

  loadMovies() {
    this.fetchMovies();
  }

  deleteMovie(id: number) {
    if (confirm('Are you sure you want to delete this movie?')) {
      // Assuming there's a delete endpoint in ApiService
      this.apiService.deleteMovie(id).subscribe({
        next: () => this.loadMovies(),
        error: (err) => alert('Failed to delete movie: ' + (err.error?.message || 'Unknown error'))
      });
    }
  }

  addMovie() {
    this.apiService.createMovie(this.newMovie).subscribe({
      next: () => {
        this.loadMovies();
        this.showAddModal = false;
        this.resetNewMovie();
      },
      error: (err) => alert('Failed to add movie: ' + (err.error?.message || 'Unknown error'))
    });
  }

  manageShowtimes(movie: Movie) {
    this.selectedMovie = movie;
    this.newShowtime.movieId = movie.id;
    this.apiService.getShowtimes(movie.id).subscribe({
      next: (res: any) => {
        this.movieShowtimes = res.data;
        this.showTimeModal = true;
      }
    });
  }

  addShowtime() {
    const combinedDateTime = `${this.newShowDate}T${this.newShowTime}:00Z`;
    const payload = { ...this.newShowtime, startTimeUtc: combinedDateTime };

    this.apiService.createShowtime(payload).subscribe({
      next: () => {
        this.manageShowtimes(this.selectedMovie!);
      },
      error: (err) => alert('Failed to add showtime: ' + (err.error?.message || 'Unknown error'))
    });
  }

  deleteShowtime(id: number) {
    if (confirm('Delete this showtime?')) {
      this.apiService.deleteShowtime(id).subscribe({
        next: () => this.manageShowtimes(this.selectedMovie!),
        error: (err) => alert('Failed to delete showtime: ' + (err.error?.message || 'Unknown error'))
      });
    }
  }

  resetNewMovie() {
    this.newMovie = {
      title: '',
      description: '',
      genre: '',
      durationMinutes: 120,
      language: 'English',
      posterUrl: '',
      releaseDate: new Date().toISOString().split('T')[0]
    };
  }
}
