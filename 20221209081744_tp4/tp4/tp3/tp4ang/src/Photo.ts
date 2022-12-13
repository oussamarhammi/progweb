import { ElementRef } from '@angular/core';
import { Galerie } from "./Galerie";

export class Photo {
        constructor(public id:number, public galerieCouverture :ElementRef  ){}
 
}
