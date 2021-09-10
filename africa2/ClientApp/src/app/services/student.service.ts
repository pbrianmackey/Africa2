import { HttpClient, HttpHeaders } from '@angular/common/http';
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

  public postStudent(student: Student): Observable<Student> {
    let httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    let options = {
      headers: httpHeaders
    };
    const body = { 
      firstName: student.firstName,
      lastName: student.lastName
    };
    
    const uri = this.localBaseUrl + 'student';

    return this.httpClient.post<Student>(uri, body, options);
  }
}
