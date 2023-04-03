import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FushionchartComponent } from './fushionchart.component';

describe('FushionchartComponent', () => {
  let component: FushionchartComponent;
  let fixture: ComponentFixture<FushionchartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FushionchartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FushionchartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
