import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Movie } from './movie';
import { MovieService } from './movie.service';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})

export class MovieDetailsComponent {

  imdbId: string;
  selectedMovie: Movie;

  constructor(private _activatedRoute: ActivatedRoute, private movieService: MovieService) { }

  ngOnInit() {
    this._activatedRoute.paramMap.subscribe(pmap => {
      this.movieService.get<Movie>(pmap.get('id'))
        .subscribe(result => {
          this.selectedMovie = result;
        }, error => console.error(error));
    })
  }
}
