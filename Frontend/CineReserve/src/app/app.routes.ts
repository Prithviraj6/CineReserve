import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { ShowtimesComponent } from './components/showtimes/showtimes.component';
import { TheaterLayoutComponent } from './components/theater-layout/theater-layout.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { RegisterComponent } from './components/register/register.component';
import { LandingComponent } from './components/landing/landing.component';
import { AdminDashboardComponent } from './components/admin/admin-dashboard.component';
import { authGuard } from './interceptors/auth.guard';

export const routes: Routes = [
    { path: '', component: LandingComponent },
    { path: 'movies', component: HomeComponent, canActivate: [authGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'admin', component: AdminDashboardComponent, canActivate: [authGuard] },
    { path: 'showtimes/:movieId', component: ShowtimesComponent, canActivate: [authGuard] },
    { path: 'seats/:showtimeId', component: TheaterLayoutComponent, canActivate: [authGuard] },
    { path: 'dashboard', component: DashboardComponent, canActivate: [authGuard] },
    { path: '**', redirectTo: '' }
];
