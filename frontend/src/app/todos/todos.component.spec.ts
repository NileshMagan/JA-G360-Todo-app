import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TodosComponent } from './todos.component';
import { TodoService } from '../services/todo.service';

describe('TodosComponent', () => {
  let component: TodosComponent;
  let fixture: ComponentFixture<TodosComponent>;
  let service: TodoService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, TodosComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(TodosComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(TodoService);
  });

  it('renders list and can add/remove', () => {
    service.items.set([{ id: '1', title: 'A', isCompleted: false }]);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelectorAll('ul.list li').length).toBe(1);

    component['newTitle'].set('B');
    spyOn(service, 'add').and.callFake((t: string) => service.items.update(l => [{ id: '2', title: t, isCompleted: false }, ...l]));
    component['add']();
    fixture.detectChanges();
    expect(compiled.querySelectorAll('ul.list li').length).toBe(2);

    spyOn(service, 'remove').and.callFake((id: string) => service.items.update(l => l.filter(i => i.id !== id)));
    component['remove']('1');
    fixture.detectChanges();
    expect(compiled.querySelectorAll('ul.list li').length).toBe(1);
  });
});


