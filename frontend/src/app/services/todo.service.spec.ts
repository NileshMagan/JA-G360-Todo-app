import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { TodoService } from './todo.service';

describe('TodoService', () => {
  let service: TodoService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [TodoService]
    });
    service = TestBed.inject(TodoService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('loads items with paging', () => {
    service.load(2, 25);

    const req = httpMock.expectOne(r => r.url === 'http://localhost:5000/api/todos');
    expect(req.request.method).toBe('GET');
    expect(req.request.params.get('page')).toBe('2');
    expect(req.request.params.get('pageSize')).toBe('25');
    req.flush([{ id: '1', title: 'A', isCompleted: false }]);

    expect(service.items().length).toBe(1);
  });

  it('adds item', () => {
    service.add('B');
    const req = httpMock.expectOne('http://localhost:5000/api/todos');
    expect(req.request.method).toBe('POST');
    req.flush({ id: '2', title: 'B', isCompleted: false });
    expect(service.items().some(i => i.title === 'B')).toBeTrue();
  });

  it('removes item', () => {
    service.items.set([{ id: '3', title: 'C', isCompleted: false }]);
    service.remove('3');
    const req = httpMock.expectOne('http://localhost:5000/api/todos/3');
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
    expect(service.items().length).toBe(0);
  });
});


