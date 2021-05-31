import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class MovieService {
 
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {
  }

  getData<ApiResult>(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterQuery: string,
    language: string,
    location: string): Observable<ApiResult> {
    const url = this.baseUrl + 'api/Movies';
    let params = new HttpParams()
      .set("pageIndex", pageIndex.toString())
      .set("pageSize", pageSize.toString())
      .set("sortColumn", sortColumn)
      .set("sortOrder", sortOrder);

    if (filterQuery) {
      params = params
        .set("filterQuery", filterQuery);
    }

    if (language && language !== "All") {
      params = params
        .set("language", language);
    }
    if (location && location !== "All") {
      params = params
        .set("location", location);
    }

    return this.http.get<ApiResult>(url, { params });
  }

  get<T>(id): Observable<T> {
    const url = this.baseUrl + "api/Movies/" + id;
    return this.http.get<T>(url);
  }

  getAvailableLanguages() {
    const url = this.baseUrl + "api/Movies/AvailableLanguages";
    return this.http.get<any>(url);
  }

  getAvailableLocations() {
    const url = this.baseUrl + "api/Movies/AvailableLocations";
    return this.http.get<any>(url);
  }
}

  export interface ApiResult<T> {
  data: T[];
  pageIndex: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
}
