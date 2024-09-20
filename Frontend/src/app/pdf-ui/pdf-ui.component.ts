import { Component, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PdfService } from './services/pdf-ui.service';
import Swal from 'sweetalert2';



@Component({
  selector: 'app-pdf-ui',
  standalone: true,
  imports: [HttpClientModule, ReactiveFormsModule, CommonModule],
  templateUrl: './pdf-ui.component.html',
  styleUrl: './pdf-ui.component.css'
})
export class PdfUIComponent implements OnInit{

  pdfs: any[] = [];
  selectedFile: File | null = null;
  currentView: string = 'upload';
  uploadStatusMessage: string = '';

  constructor(private pdfService: PdfService) {}

  ngOnInit(){
    this.loadPdfs();
  }

  loadPdfs() {
    this.pdfService.getPdfs().subscribe((data: any) => {
      this.pdfs = data;
    });
  }

  // Manejar la selección de archivo para subir
  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  // Subir el archivo seleccionado
  onUpload() {
    if (this.selectedFile) {
      // Verifica si el archivo es un PDF
      const fileName = this.selectedFile.name;
      const fileExtension = fileName.split('.').pop()?.toLowerCase();
  
      if (fileExtension !== 'pdf') {
        Swal.fire({
          title: 'Advertencia',
          text: 'El archivo seleccionado no es un PDF. Por favor, selecciona un archivo PDF.',
          icon: 'warning',
          confirmButtonText: 'Aceptar'
        }); return;
      }
  
      // Si el archivo es un PDF, procede a subirlo
      this.pdfService.uploadPdf(this.selectedFile).subscribe(
        (response: string) => {
          console.log('Respuesta del servidor:', response);
  
          Swal.fire({
            title: 'Éxito',
            text: 'PDF subido con éxito',
            icon: 'success',
            confirmButtonText: 'Aceptar'
          }).then(() => {
            this.setView('list'); 
            this.loadPdfs(); 

          });
        },
        (error) => {
          console.error('Error al subir el archivo:', error);
  
          Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "Hubo un error al subir el PDF",
            footer: '<a href="#">Why do I have this issue?</a>'
          });
        }
      );
    } else {
      Swal.fire({
        title: 'Advertencia',
        text: 'Por favor, selecciona un archivo PDF.',
        icon: 'warning',
        confirmButtonText: 'Aceptar'
      });
    }
  }

  onDownload(id: number) {
    this.pdfService.downloadPdf(id).subscribe((blob: Blob) => {
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      link.download = `pdf_${id}.pdf`;
      link.click();
    });
  }

  setView(view: string) {
    this.currentView = view;
  }

 
}
