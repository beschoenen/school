import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {MenuBarComponent} from './menu-bar.component';
import {RouterTestingModule} from '@angular/router/testing';
import {ApiService} from '../api.service';
import {ApiTestService} from '../api.test.service';

describe('MenuBarComponent', () => {
  let component: MenuBarComponent;
  let fixture: ComponentFixture<MenuBarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        MenuBarComponent
      ],
      imports: [
        RouterTestingModule
      ]
    }).overrideComponent(MenuBarComponent, {
      set: {
        providers: [{ provide: ApiService, useClass: ApiTestService }]
      }
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MenuBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
