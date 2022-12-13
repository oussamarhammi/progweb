import { Photo } from 'src/Photo';
export class Galerie {
    
   constructor(public id:number ,public name: string , public image:string , public isPublic : boolean, public photoList : Photo[],public photoCouverture: Photo){}
}
