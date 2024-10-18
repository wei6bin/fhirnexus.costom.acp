@Environment:Integration
Feature: Questionnaire

Background:
	Given a new HttpClient as httpClient
		| HeaderName               | Value   |
		| X-Ihis-SourceApplication | testapp |

Scenario: Reading a newly created Questionnaire returns the same Questionnaire
	Given a Resource is created from Samples/form-general-simple-version.json as createdResource
	When getting Questionnaire/{createdResource.Id} as readResource
	Then createdResource is a Fhir Questionnaire with data
		| Path       | Value |
		| statusCode | 201   |
	And createdResource and readResource are exactly the same
