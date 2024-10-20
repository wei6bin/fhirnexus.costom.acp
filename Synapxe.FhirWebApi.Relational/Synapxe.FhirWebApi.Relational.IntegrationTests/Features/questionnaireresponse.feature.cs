﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Synapxe.FhirWebApi.Relational.IntegrationTests.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Xunit.TraitAttribute("Category", "Environment:Integration")]
    public partial class QuestionnaireResponseFeature : object, Xunit.IClassFixture<QuestionnaireResponseFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = new string[] {
                "Environment:Integration"};
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "questionnaireresponse.feature"
#line hidden
        
        public QuestionnaireResponseFeature(QuestionnaireResponseFeature.FixtureData fixtureData, Synapxe_FhirWebApi_Relational_IntegrationTests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "QuestionnaireResponse", null, ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 4
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                        "HeaderName",
                        "Value"});
            table13.AddRow(new string[] {
                        "X-Ihis-SourceApplication",
                        "testapp"});
#line 5
 testRunner.Given("a new HttpClient as httpClient", ((string)(null)), table13, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                        "Path",
                        "Value",
                        "FhirType"});
            table14.AddRow(new string[] {
                        "url",
                        "http://synapxe.sg/Questionnaire/{#uri}",
                        "uri"});
#line 8
 testRunner.And("a Resource is created from Samples/form-general-question-nested-compare-1.json wi" +
                    "th data as createdQuestionnaire", ((string)(null)), table14, "And ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Creating a new QuestionnaireResponse")]
        [Xunit.TraitAttribute("FeatureTitle", "QuestionnaireResponse")]
        [Xunit.TraitAttribute("Description", "Creating a new QuestionnaireResponse")]
        public void CreatingANewQuestionnaireResponse()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Creating a new QuestionnaireResponse", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 12
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
                TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                            "Path",
                            "Value",
                            "FhirType"});
                table15.AddRow(new string[] {
                            "questionnaire",
                            "http://synapxe.sg/Questionnaire/{#uri}",
                            "uri"});
#line 13
 testRunner.Given("a Resource is created from Samples/form-general-answer-nested-compare-1.json with" +
                        " data as createdResponse", ((string)(null)), table15, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                            "Path",
                            "Value"});
                table16.AddRow(new string[] {
                            "statusCode",
                            "201"});
#line 16
 testRunner.Then("createdResponse is a Fhir QuestionnaireResponse with data", ((string)(null)), table16, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Creating a new QuestionnaireResponse with wrong url")]
        [Xunit.TraitAttribute("FeatureTitle", "QuestionnaireResponse")]
        [Xunit.TraitAttribute("Description", "Creating a new QuestionnaireResponse with wrong url")]
        public void CreatingANewQuestionnaireResponseWithWrongUrl()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Creating a new QuestionnaireResponse with wrong url", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 20
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
                TechTalk.SpecFlow.Table table17 = new TechTalk.SpecFlow.Table(new string[] {
                            "Path",
                            "Value",
                            "FhirType"});
                table17.AddRow(new string[] {
                            "questionnaire",
                            "http://synapxe.sg/Questionnaire/{#uri}_wrong",
                            "uri"});
#line 21
 testRunner.Given("a Resource is created from Samples/form-general-answer-nested-compare-1.json with" +
                        " data as createdResponse", ((string)(null)), table17, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table18 = new TechTalk.SpecFlow.Table(new string[] {
                            "Path",
                            "Value",
                            "FhirType"});
                table18.AddRow(new string[] {
                            "statusCode",
                            "422",
                            ""});
                table18.AddRow(new string[] {
                            "issue[0].severity",
                            "error",
                            "code"});
                table18.AddRow(new string[] {
                            "issue[0].code",
                            "not-found",
                            "code"});
                table18.AddRow(new string[] {
                            "issue[0].details.text",
                            "Resource reference \'http://synapxe.sg/Questionnaire/{#uri}_wrong\' is not valid",
                            "string"});
#line 24
 testRunner.Then("createdResponse is a Fhir OperationOutcome with data", ((string)(null)), table18, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Creating a new QuestionnaireResponse with empty answer")]
        [Xunit.TraitAttribute("FeatureTitle", "QuestionnaireResponse")]
        [Xunit.TraitAttribute("Description", "Creating a new QuestionnaireResponse with empty answer")]
        public void CreatingANewQuestionnaireResponseWithEmptyAnswer()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Creating a new QuestionnaireResponse with empty answer", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 31
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
                TechTalk.SpecFlow.Table table19 = new TechTalk.SpecFlow.Table(new string[] {
                            "Path",
                            "Value",
                            "FhirType"});
                table19.AddRow(new string[] {
                            "questionnaire",
                            "http://synapxe.sg/Questionnaire/{#uri}",
                            "uri"});
#line 32
 testRunner.Given("a Resource is created from Samples/form-general-answer-empty.json with data as cr" +
                        "eatedResponse", ((string)(null)), table19, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table20 = new TechTalk.SpecFlow.Table(new string[] {
                            "Path",
                            "Value"});
                table20.AddRow(new string[] {
                            "statusCode",
                            "422"});
#line 35
 testRunner.Then("createdResponse is a Fhir OperationOutcome with data", ((string)(null)), table20, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                QuestionnaireResponseFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                QuestionnaireResponseFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
