@Environment:Integration
Feature: QuestionnaireResponse

Background:
	Given a new HttpClient as httpClient
		| HeaderName               | Value   |
		| X-Ihis-SourceApplication | testapp |
	And a Resource is created from Samples/form-general-question-nested-compare-1.json with data as createdQuestionnaire
		| Path | Value                                  | FhirType |
		| url  | http://synapxe.sg/Questionnaire/{#uri} | uri      |

Scenario: Creating a new QuestionnaireResponse
	Given a Resource is created from Samples/form-general-answer-nested-compare-1.json with data as createdResponse
		| Path          | Value                                  | FhirType |
		| questionnaire | http://synapxe.sg/Questionnaire/{#uri} | uri      |
	Then createdResponse is a Fhir QuestionnaireResponse with data
		| Path       | Value |
		| statusCode | 201   |

Scenario: Creating a new QuestionnaireResponse with wrong url
	Given a Resource is created from Samples/form-general-answer-nested-compare-1.json with data as createdResponse
		| Path          | Value                                        | FhirType |
		| questionnaire | http://synapxe.sg/Questionnaire/{#uri}_wrong | uri      |
	Then createdResponse is a Fhir OperationOutcome with data
		| Path                  | Value                                                                          | FhirType |
		| statusCode            | 422                                                                            |          |
		| issue[0].severity     | error                                                                          | code     |
		| issue[0].code         | not-found                                                                      | code     |
		| issue[0].details.text | Resource reference 'http://synapxe.sg/Questionnaire/{#uri}_wrong' is not valid | string   |

Scenario: Creating a new QuestionnaireResponse with empty answer
	Given a Resource is created from Samples/form-general-answer-empty.json with data as createdResponse
		| Path          | Value                                  | FhirType |
		| questionnaire | http://synapxe.sg/Questionnaire/{#uri} | uri      |
	Then createdResponse is a Fhir OperationOutcome with data
		| Path       | Value |
		| statusCode | 422   |
