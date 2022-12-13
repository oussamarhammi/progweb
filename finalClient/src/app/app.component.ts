import { HttpClient, HttpHeaderResponse, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Message } from './models/message';
import { Thread } from './models/thread';
import { UserMessageDTO } from './models/userMessageDTO';
import { HttpService } from './services/http.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  register = {username : "", email : "", password : "", passwordConfirm : ""};
  login = {username : "", password : "", Email: ""};
  
  threads : Thread[] = [];
  messages : UserMessageDTO[] = [];
  messagesRecherche : Message[] = [];
  
  // Id et titre du fil de disccusion dont les messages sont affichés présentement
  currentThread : string = "";
  currentThreadId : number = 0;

  nouveauMessage : string = "";

  rechercherUtilisateur : string = "";
  
  constructor(public httpService : HttpService,public http : HttpClient){}

  ngOnInit(): void {
    this.refreshThreads();
  }

  async refreshThreads() : Promise<void> {
    this.threads = await this.httpService.getThreads();
  }

  async displayMessages(id : number, title : string) : Promise<void> {
    this.currentThread = title;
    this.currentThreadId = id;
    this.messages = await this.httpService.getMessages(id);
  }

  addMessage() : void {
    let newMessage = new Message(0, this.nouveauMessage);
    let token = localStorage.getItem("authToken");
    let httpOptions = {
      headers : new HttpHeaders({
        'Content-Type' : 'application/json',
        'Authorization' : 'Bearer ' + token
      })
    };
    this.http.post<Message>("https://localhost:5001/api/Messages/PostMessage/"+this.currentThreadId ,newMessage, httpOptions ).subscribe(x=>{
      console.log(x);
      //this.messages.push(x);
    
     
      
    })
  }

  postMessage(id : number, text : string) : void{
    let message = {id : 0, text : text};
    this.http.post<any>(domain + "api/Messages/PostMessage/" + id, message).subscribe(x => {
      console.log(x);
      this.getMessages(id);
    });
  } 

  searchMessages() : void {
    // À COMPLÉTER
    // La variable this.messagesRecherche doit être remplie avec la liste des messages de l'utilisateur
    let token = localStorage.getItem("token");
    let httpOptions = {
      headers : new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization' : 'Bearer ' + token
      })
    };

    this.http.get<Message[]>("https://localhost:5001/api/Messages/GetUserMessages/"+this.rechercherUtilisateur  , httpOptions).subscribe(
      x=>{
      console.log(x);
      this.messagesRecherche= x;
    })

  }

}
