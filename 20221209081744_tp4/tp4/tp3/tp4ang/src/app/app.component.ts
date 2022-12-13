
import { Galerie } from './../Galerie';
import { Component, ElementRef, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import{ HttpClient, HttpEventType, HttpHeaders} from '@angular/common/http';
import { Photo } from 'src/Photo';
import { OwlOptions } from 'ngx-owl-carousel-o';



declare var Masonry : any;
declare var imagesLoaded : any;
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent {
  title = 'tp3';
  username: String="";
  email: string = "";
  password : string="";
  passwordConfirm:string = "";
  name:string="";
  image:string="";
  isPublic:boolean= false;
  Galerie: Galerie[] = [];
  GalPub: Galerie[] = []
  Photo : Photo[] = [];
  chosenGalerie ?: Galerie ;


  @ViewChild('masongrid') masongrid?: ElementRef; 
  @ViewChildren('masongriditems') masongriditems?: QueryList<any>; 
  
  
  customOptions: OwlOptions = {
    loop: true, mouseDrag: false, touchDrag: false, pullDrag:false,dots:false,
    navSpeed:700,
    navText: ['◀', '▶'],
    responsive: {0: {items: 1},400: {items: 2},740: {items: 3},940: {items: 4}},
    nav: true
  }
    




  constructor(public http : HttpClient){}

  ngAfterViewInit(){
    this.masongriditems?.changes.subscribe(e=>{
      this.initMasonry();
    });

    if(this.masongriditems!.length >0){
      this.initMasonry();
    }
  }
  initMasonry(){
    var grid = this.masongrid?.nativeElement;
    var msnry = new Masonry(grid,{
      itemSelector : '.grid-item'
    });

    imagesLoaded(grid).on('progress', function(){
      msnry.layout();
    });
  }


  registerUser(){
    let user = {
      username: this.username,
      email:this.email,
      password: this.password,
      passwordConfirm : this.passwordConfirm
    };

    this.http.post<any>("https://localhost:5001/api/Users/Register", user).subscribe(x=>{
      console.log(x);
    })
  }

  loginUser(){
    let user = {
      username: this.username,
      password: this.password,
    };
    this.http.post<any>("https://localhost:5001/api/Users/Login", user).subscribe(x=>{
      console.log(x);
      localStorage.setItem("authToken", x.token);
    })
  }

  CreerGalerie(): void{
    console.log("ajout galerie");
    let galerie ={
      id :0,
      name :this.name,
      image: this.image,
      isPublic : this.isPublic
    }
    let token = localStorage.getItem("authToken");
    let httpOptions = {
      headers : new HttpHeaders({
        'Content-Type' : 'application/json',
        'Authorization' : 'Bearer ' + token
      })
    };
    
     this.http.post<Galerie>("https://localhost:5001/api/Galeries/" ,galerie, httpOptions ).subscribe(x=>{
      console.log(x);
      this.Galerie.push(x);
      this.addCover(x.id,this.couvertureupload);
      
    })

   
  }

  addCover(id:number, photoinp ?:ElementRef) :void{
    if(photoinp != undefined && photoinp.nativeElement.files.length>0){
    console.log("cover set")
    let file = photoinp.nativeElement.files[0];
    let formData = new FormData();
    formData.append("photo", file, file.name);
    
    this.http.post<Photo>("https://localhost:5001/api/photos/couverture/"+id,formData ).subscribe(x=>{
      console.log(x);
      
      
    })
  }
  }

  changer(id:number) :void{
    console.log("coucou")
    if(this.couverturechanged != undefined && this.couverturechanged.nativeElement.files.length>0){
     
    let file = this.couverturechanged.nativeElement.files[0];
    let formData = new FormData();
    formData.append("photo", file, file.name);
    console.log("bruh")
    
    this.http.post<any>("https://localhost:5001/api/photos/couvertureChange/"+id,formData).subscribe(x=>{
      console.log(x);
      
      
    })
  }
  }
  getGalerie(){
    let token = localStorage.getItem("authToken");
    console.log("cover set")
    let httpOptions = {
      headers : new HttpHeaders({
        'Content-Type' : 'application/json',
        'Authorization' : 'Bearer ' + token
      })
    };
    this.http.get<Galerie[]>("https://localhost:5001/api/Galeries/",httpOptions).subscribe(x=>{
      console.log(x);
      this.Galerie = x;
    })
  
  }

  deleteGalerie(id:number)  : void{
    let token = localStorage.getItem("authToken");
    let httpOptions = {
      headers : new HttpHeaders({
        'Content-Type' : 'application/json',
        'Authorization' : 'Bearer ' + token
      })
    }
        this.http.delete<Galerie[]>("https://localhost:5001/api/Galeries/"+ id, httpOptions).subscribe(
          x=>console.log(x)
        );
      }
  
      deletePhoto(id:number)  : void{
        let token = localStorage.getItem("authToken");
        let httpOptions = {
          headers : new HttpHeaders({
            'Content-Type' : 'application/json',
            'Authorization' : 'Bearer ' + token
          })
        }
            this.http.delete<Photo[]>("https://localhost:5001/api/photos/"+ id, httpOptions).subscribe(
              x=>console.log(x)
            );
          }

  AjoutPropr(id:number){
    let Prop = {
      nomUtil: this.name,
    }
    let token = localStorage.getItem("authToken");
    let httpOptions = {
      headers : new HttpHeaders({
        'Content-Type' : 'application/json',
        'Authorization' : 'Bearer ' + token
      })
    };
     this.http.post<any>("https://localhost:5001/api/Galeries/"+id,Prop, httpOptions ).subscribe(x=>{
      console.log(x);
     
    })
  }

  getGaleriePublique(){
    let token = localStorage.getItem("authToken");
    let httpOptions = {
      headers : new HttpHeaders({
        'Content-Type' : 'application/json',
        'Authorization' : 'Bearer ' + token
      })
    };
     this.http.get<Galerie[]>("https://localhost:5001/api/Galeries/publique" , httpOptions ).subscribe(x=>{
      console.log(x);
      this.GalPub = x;
     
    })
  }

  logoutUser(){
    localStorage.removeItem("authToken");
  }

  chooseGalerie(galerie: Galerie){
     this.chosenGalerie = galerie;
  }

 
  @ViewChild("fileuploadviewchild", {static:false}) fileuploadviewchild ?:ElementRef;
  progress : number= 0;

  @ViewChild("couvertureupload", {static:false}) couvertureupload ?:ElementRef;
  @ViewChild("couverturechanged", {static:false}) couverturechanged ?:ElementRef;

  uploadViewChild(id: number): void{
    if(this.fileuploadviewchild != undefined){
      let file = this.fileuploadviewchild.nativeElement.files[0];
      let formData = new FormData();
      formData.append('image', file, file.name);
      this.http.post<any>("https://localhost:5001/api/photos/"+id, formData,
      {reportProgress:true, observe:"events"}).subscribe(x =>{
        if(x != undefined)
        {
          if(x.type === HttpEventType.UploadProgress && x.total != undefined){
            this.progress = Math.round(100* x.loaded/ x.total);
          }
          else if(x.type === HttpEventType.Response){
              console.log("photo chargéee !");
              this.progress = 100;
          }
        }
      })
    }
  }

   
  
        

  


}


