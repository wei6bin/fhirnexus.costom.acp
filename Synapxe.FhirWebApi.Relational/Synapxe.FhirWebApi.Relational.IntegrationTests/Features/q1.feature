@Environment:Integration
Feature: q1

Background:
	Given a new HttpClient as httpClient
		| HeaderName               | Value   |
		| X-Ihis-SourceApplication | testapp |
	And a Resource is created from Samples/1.1_questionnaire.json with data as createdQuestionnaire
		| Path    | Value                                           | FhirType |
		| url     | http://fhir.synapxe.sg/acp/Questionnaire/{#uri} | uri      |
		| version | 1.0.0                                           | string   |

Scenario: Create a new QuestionnaireResponse successfully
	Given a Resource is created from Samples/1.2_questionnaireresponse.json with data as createdResponse
		| Path          | Value                                                  | FhirType |
		| questionnaire | http://fhir.synapxe.sg/acp/Questionnaire/{#uri}\|1.0.0 | uri      |
	Then createdResponse is a Fhir QuestionnaireResponse with data
		| Path       | Value |
		| statusCode | 201   |

Scenario: Create a new QuestionnireResponse by providing a wrong business version
	Given a Resource is created from Samples/1.2_questionnaireresponse.json with data as createdResponse
		| Path          | Value                                                  | FhirType |
		| questionnaire | http://fhir.synapxe.sg/acp/Questionnaire/{#uri}\|1.0.1 | uri      |
	Then createdResponse is a Fhir OperationOutcome with data
		| Path       | Value |
		| statusCode | 422   |