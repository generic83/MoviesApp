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
  deultSortColumn = "title";
  
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  constructor(private movieService: MovieService) {
  }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    const pageEvent = new PageEvent();
    pageEvent.pageIndex = this.defaultPageIndex;
    pageEvent.pageSize = this.defaultPageSize;
    this.getData(pageEvent);
  }

  getData(event: PageEvent) {
    this.movieService.getData<ApiResult<Movie>>(
      event.pageIndex,
      event.pageSize)
      .subscribe(result => {
        this.paginator.length = result.totalCount;
        this.paginator.pageIndex = result.pageIndex;
        this.paginator.pageSize = result.pageSize;
        this.movies = new MatTableDataSource<Movie>(result.data);
      }, error => console.error(error));
  }
}