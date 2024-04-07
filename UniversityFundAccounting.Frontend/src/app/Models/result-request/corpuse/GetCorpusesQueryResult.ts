import { ICorpuse } from "../../ICorpuse";
import { IValidationResult } from "../../IValidationResult";

export interface GetCorpusesQueryResult {
  validationResult : IValidationResult,
  objResult : ICorpuse[]
}