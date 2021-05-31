import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from '../angular-material.module';
import { of, ReplaySubject } from 'rxjs';
import { MovieDetailsComponent } from './movie-details.component';
import { Movie } from './movie';
import { MovieService } from './movie.service';
import { ActivatedRoute, Params, ParamMap, convertToParamMap } from '@angular/router';

export class ActivatedRouteStub {
  private subject = new ReplaySubject<ParamMap>();

  constructor(initialParams?: Params) {
    this.setParamMap(initialParams);
  }

  readonly paramMap = this.subject.asObservable();

  setParamMap(params?: Params) {
    this.subject.next(convertToParamMap(params));
  }
}

describe('MoviesDetailsComponent', () => {
  let fixture: ComponentFixture<MovieDetailsComponent>;

  // async beforeEach()
  beforeEach(async(() => {
    const movieService = jasmine.createSpyObj<MovieService>('MovieService',
      ['get']);
    movieService.get.withArgs('imdbid1').and.returnValue(
      of<Movie>(
          {
            imdbId: 'imdbid1',
            title: 'Test Title1',
            language: 'TestLanguage1',
            location: 'TestLocation1',
            imdbRating: '9.1',
            soundEffects: ['sf1', 'sf2'],
            plot: 'plot',
            listingType: 'listingType',
            poster: 'imgSource',
            stills: ['imgSource1', 'imgSource2']
          } as Movie));

    const activateRoute = new ActivatedRouteStub({ id: 'imdbid1' });

    TestBed.configureTestingModule({
      declarations: [MovieDetailsComponent],
      imports: [
        BrowserAnimationsModule,
        AngularMaterialModule
      ],
      providers: [
        {
          provide: MovieService,
          useValue: movieService
        },
        {
          provide: ActivatedRoute,
          useValue: activateRoute
        }
      ]
    })
      .compileComponents();
  }));
  // synchronous beforeEach()
  beforeEach(() => {
    fixture = TestBed.createComponent(MovieDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  // tests
  it('should display all movie-related fields',
    async(() => {
      const detailsElements = fixture.nativeElement
        .querySelectorAll('p');
      expect(detailsElements.length).toEqual(7);

      const expectedFields = ['Title', 'Language', 'Location', 'Listing Type', 'Imdb Rating', 'Sound Effect', 'Plot'];
      detailsElements.forEach(x => {
        expect(expectedFields.indexOf(x) > 1)
      });
    }));

  it('should display "Test Title1" title',
    async(() => {
      const title = fixture.nativeElement
        .querySelectorAll('p')[0];
      expect(title.textContent).toContain('Test Title1');
    }));

  it('should contain three img tags',
    async(() => {
      const images = fixture.nativeElement
        .querySelectorAll('img');
      expect(images.length).toEqual(3);
    }));

});
