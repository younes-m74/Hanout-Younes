import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-test-error',
  standalone: true,
  imports: [
    MatButton,
    CommonModule
  ],
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.scss'
})
export class TestErrorComponent {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  validationErrors?: string[];

  get404Error() {
    this.http.get(this.baseUrl + 'bugs/notfound').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

  get400Error() {
    this.http.get(this.baseUrl + 'bugs/badrequest').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

  get401Error() {
    this.http.get(this.baseUrl + 'bugs/unauthorized').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

  get500Error() {
    this.http.get(this.baseUrl + 'bugs/internalerror').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

  get400ValidationError() {
    this.http.post(this.baseUrl + 'bugs/validationerror', {}).subscribe({
      next: response => console.log(response),
      error: error => this.validationErrors = error
    })
  }
}
