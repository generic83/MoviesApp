import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from '../angular-material.module';
import { of } from 'rxjs';
import { MoviesComponent } from './movies.component';
import { Movie } from './movie';
import { MovieService, ApiResult } from './movie.service';
import { Router } from '@angular/router';

describe('MoviesComponent', () => {
  let fixture: ComponentFixture<MoviesComponent>;
  let component: MoviesComponent;

  // async beforeEach()
  beforeEach(async(() => {
    const movieService = jasmine.createSpyObj<MovieService>('MovieService',
      ['getData', 'getAvailableLanguages', 'getAvailableLocations']);
    movieService.getData.and.returnValue(
      of<ApiResult<Movie>>({
        data: [
          {
            imdbId: 'imdbid1',
            title: 'Test Title1',
            language: 'TestLanguage1',
            location: 'TestLocation1',
            imdbRating: '9.1'
          },
          {
            imdbId: 'imdbid2',
            title: 'Test Title2',
            language: 'TestLanguage2',
            location: 'TestLocation2',
            imdbRating: '7.1'
          },
          {
            imdbId: 'imdbid3',
            title: 'Test Title3',
            language: 'TestLanguage3',
            location: 'TestLocation3',
            imdbRating: '10'
          }
        ],
        totalCount: 3,
        pageIndex: 0,
        pageSize: 10
      } as ApiResult<Movie>));

    movieService.getAvailableLanguages.and.returnValue(of<any>(["TestLanguage1", "TestLanguage2", "TestLanguage3"]));
    movieService.getAvailableLocations.and.returnValue(of<any>(["TestLocation1", "TestLocation2", "TestLocation3"]));

    const router = jasmine.createSpyObj<Router>('Router',['navigateByUrl']);

    TestBed.configureTestingModule({
      declarations: [MoviesComponent],
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
          provide: Router,
          useValue: router
        }
      ]
    })
      .compileComponents();
  }));
  // synchronous beforeEach()
  beforeEach(() => {
    fixture = TestBed.createComponent(MoviesComponent);
    component = fixture.componentInstance;
    component.paginator = jasmine.createSpyObj(
      "MatPaginator", ["length", "pageIndex", "pageSize"]
    );
    component.sort = jasmine.createSpyObj(
      "MatSort", ["disableClear"]
    );
    fixture.detectChanges();
  });

  // tests
  it('should contain a title input filter', async(() => {
    const titleFilter = fixture.nativeElement
      .querySelector('.mat-form-field-type-mat-input');
    expect(titleFilter.textContent).toEqual('Filter by title (or part of it)...');
  }));

  it('should contain a table with at list of one or more movies',
    async(() => {
      const table = fixture.nativeElement
        .querySelector('table.mat-table');
      const tableRows = table
        .querySelectorAll('tr.mat-row');
      expect(tableRows.length).toBeGreaterThan(0);
    }));

  it('should contain a language select filter', async(() => {
    const languageSelectFilter = fixture.nativeElement
      .querySelectorAll('.mat-form-field-type-mat-select')[0];
    expect(languageSelectFilter.textContent).toContain('select language');
  }));

  it('should contain location select filter', async(() => {
    const locationSelectFilter = fixture.nativeElement
      .querySelectorAll('.mat-form-field-type-mat-select')[1];
    expect(locationSelectFilter.textContent).toContain('select location');
  }));
});
