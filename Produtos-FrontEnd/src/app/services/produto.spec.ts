import { TestBed } from '@angular/core/testing';

class Produto {}

describe('Produto', () => {
  let service: Produto;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Produto],
    });
    service = TestBed.inject(Produto);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
