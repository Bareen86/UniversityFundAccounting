import { AudienceType } from "../../AudienceType";

export interface ICreateAudience {
  corpuseId : number;
  name : string;
  audienceType : AudienceType;
  capacity : number;
  floor : number;
  audienceNumber : number
}