import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { ConfigService } from './config.service';

describe('ConfigService', () => {
  let service: ConfigService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ConfigService]
    });
    service = TestBed.inject(ConfigService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('loads config', (done) => {
    const mockConfig = { apiBaseUrl: 'http://test-api.com' };
    
    service.loadConfig().then(() => {
      expect(service.getConfig()).toEqual(mockConfig);
      done();
    });
    
    const req = httpMock.expectOne('/assets/config/config.json');
    expect(req.request.method).toBe('GET');
    req.flush(mockConfig);
  });

  it('throws if config not loaded', () => {
    expect(() => service.getConfig()).toThrowError('Config not loaded');
  });
});
