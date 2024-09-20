import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PdfService {

  private baseUrl = 'http://localhost:7260/api/PdfDocument'; // Reemplaza con tu URL

  constructor(private http: HttpClient) {}

  // Método para subir un archivo PDF
  uploadPdf(pdfFile: File): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('pdf', pdfFile, pdfFile.name);
    return this.http.post(`${this.baseUrl}/upload`, formData, { responseType: 'text' });
  }

  // Método para obtener todos los PDFs
  getPdfs(): Observable<any> {
    return this.http.get(`${this.baseUrl}/all`);
  }

  // Método para descargar un PDF por su ID
  downloadPdf(id: number): Observable<Blob> {
    return this.http.get(`${this.baseUrl}/download/${id}`, { responseType: 'blob' });
  }

}
