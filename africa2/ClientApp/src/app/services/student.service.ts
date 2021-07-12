import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Student } from '../model/student.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private localBaseUrl: string;

  constructor(
    private httpClient: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
    ) { 
      this.localBaseUrl = baseUrl;
    }

  public getStudents(): Observable<Student[]> {
    return this.httpClient.get<Student[]>(this.localBaseUrl + 'student');
  }
}
