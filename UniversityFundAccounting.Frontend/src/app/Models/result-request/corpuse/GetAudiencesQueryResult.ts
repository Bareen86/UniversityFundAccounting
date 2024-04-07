import { IAudience } from "../../IAudience";
import { IValidationResult } from "../../IValidationResult";

export interface GetAudiencesQueryResult {
  validationResult : IValidationResult,
  objResult : IAudience[]
}