export interface Movie {
    id: number;
    title: string;
    description: string;
    durationMinutes: number;
    genre: string;
    language: string;
    posterUrl: string;
    releaseDate: string;
}

export interface ShowTime {
    id: number;
    movieId: number;
    theaterHallId: number;
    movieTitle: string;
    theaterHallName: string;
    startTimeUtc: string;
    basePrice: number;
}

export interface Seat {
    seatId: number;
    rowLabel: string;
    seatNumber: number;
    seatType: string;
    price: number;
    isAvailable: boolean;
    movieTitle: string;
    theaterHallName: string;
    selected?: boolean; // UI only property
}

export interface BookingResponse {
    id: number;
    bookingReference: string;
    totalAmount: number;
    bookedAtUtc: string;
}

export interface LoginResponse {
    userId: number;
    fullName: string;
    email: string;
    role: string;
    token: string;
    creditBalance: number;
}
