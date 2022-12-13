import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { UserMessageDTO } from '../models/userMessageDTO';
import { Thread } from '../models/thread';

const domain = "https://localhost:5001/";

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(public http : HttpClient) { }

  // Obtenir la liste des fils de discussion
  async getThreads() : Promise<Thread[]>{
    console.log("GET THREADS :");
    let res = await lastValueFrom(this.http.get<Thread[]>(domain + "api/Threads/GetThread"));
    console.log(res);
    return res;
  }

  // Obtenir les messages d'un fil de discussion
  async getMessages(id : number) : Promise<UserMessageDTO[]>{
    console.log("GET MESSAGES :");
    let res = await lastValueFrom(this.http.get<UserMessageDTO[]>(domain + "api/Messages/GetThreadMessages/" + id));
    console.log(res);
    return res;
  }



  // S'inscrire
  register(dto : any) : void{
    console.log("REGISTER :");
    this.http.post<any>(domain + "api/Users/Register", dto).subscribe(x => {
      console.log(x);
    })
  }

  // Se connecter
  login(dto : any) : void{
    console.log("LOGIN :");
    this.http.post<any>(domain + "api/Users/Login", dto).subscribe(x => {
      console.log(x);
      localStorage.setItem("token", x.token);
    })
  }

}
