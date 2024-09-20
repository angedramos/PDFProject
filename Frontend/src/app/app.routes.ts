import { Routes } from '@angular/router';
import { PdfUIComponent } from './pdf-ui/pdf-ui.component';

export const routes: Routes = [
    {
        path:'',
        component:PdfUIComponent
    },
    {
        path:'**',
        redirectTo:''
    }
];
