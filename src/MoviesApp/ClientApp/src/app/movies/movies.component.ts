import { Component, ViewChild, OnInit } from '@angular/core';
import { Movie } from './movie';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ApiResult, MovieService } from './movie.service';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})

export class MoviesComponent implements OnInit {
  public movies: MatTableDataSource<Movie>;
  public displayedColumns: string[] = ['imdbId', 'title', 'language', 'location', 'imdbRating'];

  defaultPageIndex = 0;
  defaultPageSize = 10;
  defaultSortColumn = "title";
  defaultSortOrder = "asc";
  filterQuery: string = null;

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private movieService: MovieService) {
  }

  ngOnInit() {
    this.loadData();
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
      filterQuery)
      .subscribe(result => {
        this.sort.disableClear = true;
        this.paginator.length = result.totalCount;
        this.paginator.pageIndex = result.pageIndex;
        this.paginator.pageSize = result.pageSize;
        this.movies = new MatTableDataSource<Movie>(result.data);
      }, error => console.error(error));
  }
}
