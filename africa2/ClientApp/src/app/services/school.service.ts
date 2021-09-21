import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { School } from '../model/school.model';

@Injectable({
  providedIn: 'root'
})
export class SchoolService {

  private localBaseUrl: string;

  constructor(
    private httpClient: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {
    this.localBaseUrl = baseUrl;
  }

  public getSchools(): Observable<School[]> {
    return this.httpClient.get<School[]>(this.localBaseUrl + 'school');
  }

  public postSchool(school: School): Observable<School> {
    let httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    let options = {
      headers: httpHeaders
    };
    const body = { 
      name: school.name,
      address: school.address
    };
    
    const uri = this.localBaseUrl + 'school';

    return this.httpClient.post<School>(uri, body, options);
  }
}