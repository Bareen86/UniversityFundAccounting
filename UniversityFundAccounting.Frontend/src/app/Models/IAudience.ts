import { AudienceType } from "./AudienceType";

export interface IAudience {
  id : number,
  corpuseId : number,
  name : string,
  audienceType : AudienceType,
  capacity : number,
  floor : number,
  audienceNumber : number
}