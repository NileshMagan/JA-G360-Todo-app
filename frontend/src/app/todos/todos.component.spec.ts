import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TodosComponent } from './todos.component';
import { TodoService } from '../services/todo.service';
import { ConfigService } from '../services/config.service';
import { MatCardModule } from '@angular/material/card';
import { DragDropModule } from '@angular/cdk/drag-drop';

describe('TodosComponent', () => {
  let component: TodosComponent;
  let fixture: ComponentFixture<TodosComponent>;
  let service: TodoService;

  beforeEach(async () => {
    const configSpy = jasmine.createSpyObj('ConfigService', ['getConfig', 'loadConfig']);
    configSpy.getConfig.and.returnValue({ apiBaseUrl: 'http://test-api.com/api' });
    configSpy.loadConfig.and.returnValue(Promise.resolve());

    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, TodosComponent, MatCardModule, DragDropModule],
      providers: [
        { provide: ConfigService, useValue: configSpy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(TodosComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(TodoService);
  });

  it('shows empty state alert when no items', () => {
    service.items.set([]);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    const infoAlert = compiled.querySelector('.info-alert');
    expect(infoAlert).toBeTruthy();
    expect(infoAlert?.textContent?.trim()).toContain('No todos added');
  });

  it('renders list and can add/remove', () => {
    service.items.set([{ id: '1', title: 'A', isCompleted: false }]);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelectorAll('.list-row').length).toBe(1);

    component['newTitle'].set('B');
    spyOn(service, 'add').and.callFake((t: string) => service.items.update(l => [{ id: '2', title: t, isCompleted: false }, ...l]));
    component['add']();
    fixture.detectChanges();
    expect(compiled.querySelectorAll('.list-row').length).toBe(2);

    spyOn(service, 'remove').and.callFake((id: string) => service.items.update(l => l.filter(i => i.id !== id)));
    component['remove']('1');
    fixture.detectChanges();
    expect(compiled.querySelectorAll('.list-row').length).toBe(1);
  });
});


