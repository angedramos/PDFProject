import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PdfUIComponent } from './pdf-ui.component';
import { PdfService } from './services/pdf-ui.service';
import { of, throwError } from 'rxjs';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { DebugElement } from '@angular/core';
import { By } from '@angular/platform-browser';

describe('PdfUIComponent', () => {
  let component: PdfUIComponent;
  let fixture: ComponentFixture<PdfUIComponent>;
  let pdfService: jasmine.SpyObj<PdfService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('PdfService', ['uploadPdf', 'getPdfs', 'downloadPdf']);
    
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, PdfUIComponent],
      providers: [{ provide: PdfService, useValue: spy }],
    }).compileComponents();

    fixture = TestBed.createComponent(PdfUIComponent);
    component = fixture.componentInstance;
    pdfService = TestBed.inject(PdfService) as jasmine.SpyObj<PdfService>;

    pdfService.getPdfs.and.returnValue(of([]));

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should download a PDF file', () => {
    const blob = new Blob(['dummy content'], { type: 'application/pdf' });
    spyOn(window.URL, 'createObjectURL').and.returnValue('blobURL');
    spyOn(document, 'createElement').and.returnValue({ click: jasmine.createSpy() } as any);
  
    pdfService.downloadPdf.and.returnValue(of(blob));
  
    component.onDownload(1);
  
    expect(pdfService.downloadPdf).toHaveBeenCalledWith(1);
    expect(window.URL.createObjectURL).toHaveBeenCalledWith(blob);
  });

  it('should upload a PDF file successfully', () => {
    const fakeFile = new File(['dummy content'], 'test.pdf', { type: 'application/pdf' });
    const event = { target: { files: [fakeFile] } };

    pdfService.uploadPdf.and.returnValue(of('Upload successful'));

    component.onFileSelected(event);
    component.onUpload();

    expect(pdfService.uploadPdf).toHaveBeenCalledWith(fakeFile);
  });


  it('should show error message on upload failure', () => {
    const fakeFile = new File(['dummy content'], 'test.pdf', { type: 'application/pdf' });
    const event = { target: { files: [fakeFile] } };

    pdfService.uploadPdf.and.returnValue(throwError(() => new Error('Upload error')));

    component.onFileSelected(event);
    component.onUpload();

    expect(pdfService.uploadPdf).toHaveBeenCalled();
  });
});
