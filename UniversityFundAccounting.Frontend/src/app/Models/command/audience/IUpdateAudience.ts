import { AudienceType } from "../../AudienceType";

export interface IUpdateAudience {
  corpuseId : number;
  name : string;
  audienceType : AudienceType;
  capacity : number;
  floor : number;
  audienceNumber : number
}