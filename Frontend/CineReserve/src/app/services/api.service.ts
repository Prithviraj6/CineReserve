import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Movie, ShowTime, Seat, BookingResponse, LoginResponse } from '../models/api.models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // Auth
  login(credentials: any): Observable<{ data: LoginResponse }> {
    return this.http.post<{ data: LoginResponse }>(`${this.baseUrl}/Auth/login`, credentials);
  }

  register(userData: any): Observable<{ data: LoginResponse }> {
    return this.http.post<{ data: LoginResponse }>(`${this.baseUrl}/Auth/register`, userData);
  }

  topUp(amount: number) {
    return this.http.post(`${this.baseUrl}/users/topup`, amount);
  }

  // Movies
  getMovies(): Observable<{ data: Movie[] }> {
    return this.http.get<{ data: Movie[] }>(`${this.baseUrl}/Movies`);
  }

  createMovie(movie: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Movies`, movie);
  }

  deleteMovie(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Movies/${id}`);
  }

  // Showtimes
  getShowtimes(movieId: number): Observable<{ data: ShowTime[] }> {
    return this.http.get<{ data: ShowTime[] }>(`${this.baseUrl}/Showtimes?movieId=${movieId}`);
  }

  createShowtime(showtime: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Showtimes`, showtime);
  }

  deleteShowtime(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Showtimes/${id}`);
  }

  // Seats
  getSeats(showtimeId: number): Observable<{ data: Seat[] }> {
    return this.http.get<{ data: Seat[] }>(`${this.baseUrl}/Seats/${showtimeId}`);
  }

  // Bookings
  createBooking(booking: { showtimeId: number, seats: { seatId: number }[] }): Observable<{ data: BookingResponse }> {
    return this.http.post<{ data: BookingResponse }>(`${this.baseUrl}/Bookings`, booking);
  }

  getMyBookings(): Observable<{ data: any[] }> {
    return this.http.get<{ data: any[] }>(`${this.baseUrl}/Bookings/mine`);
  }
}
