
// Cette classe ne doit pas être utilisée pour créer un nouveau message. Elle sert seulement à recevoir les informations d'un message par le serveur.

export class UserMessageDTO{
    constructor(
        public username : string, 
        public userId : string, 
        public messageCount : number, 
        public text : string, 
        public hasAvatar : boolean){}
}