import { Component, ViewChild, OnInit } from '@angular/core';
import { Movie } from './movie';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ApiResult, MovieService } from './movie.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})

export class MoviesComponent implements OnInit {
  public movies: MatTableDataSource<Movie>;
  public displayedColumns: string[] = ['title', 'language', 'location', 'imdbRating', 'poster'];

  defaultPageIndex = 0;
  defaultPageSize = 5;
  defaultSortColumn = "title";
  defaultSortOrder = "asc";
  filterQuery: string = null;
  availableLanguages: string[] = null;
  availableLocations: string[] = null;
  language: string = null;
  location: string = null;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private movieService: MovieService, private _router: Router) {
  }

  ngOnInit() {
    this.loadData();
    this.populateSelectFilters();
  }


  populateSelectFilters() {
    this.movieService.getAvailableLanguages()
      .subscribe(data => {
        this.availableLanguages = data;
      }, error => console.error(error));
    this.movieService.getAvailableLocations()
      .subscribe(data => {
        this.availableLocations = data;
      }, error => console.error(error));
  }

  loadData(query: string = null) {
    const pageEvent = new PageEvent();
    pageEvent.pageIndex = this.defaultPageIndex;
    pageEvent.pageSize = this.defaultPageSize;
    query ? this.filterQuery = query : this.filterQuery = null;
    this.getData(pageEvent);
  }

  getData(event: PageEvent) {
    const sortColumn = (this.sort)
      ? this.sort.active
      : this.defaultSortColumn;
    const sortOrder = (this.sort)
      ? this.sort.direction
      : this.defaultSortOrder;
    const filterQuery = (this.filterQuery)
      ? this.filterQuery
      : null;

    this.movieService.getData<ApiResult<Movie>>(
      event.pageIndex,
      event.pageSize,
      sortColumn,
      sortOrder,
      filterQuery,
      this.language,
      this.location)
      .subscribe(result => {
        this.sort.disableClear = true;
        this.paginator.length = result.totalCount;
        this.paginator.pageIndex = result.pageIndex;
        this.paginator.pageSize = result.pageSize;
        this.movies = new MatTableDataSource<Movie>(result.data);
      }, error => console.error(error));
  }

  onLanguageChange(event) {
    this.language = event.value;
    this.loadData(this.filterQuery);
  }

  onLocationChange(event) {
    this.location = event.value;
    this.loadData(this.filterQuery);
  }

  onRowClick(row: Movie) {
    this._router.navigateByUrl(`details/${row.id}`);
  }
}
