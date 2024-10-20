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
    public partial class QuestionnaireFeature : object, Xunit.IClassFixture<QuestionnaireFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = new string[] {
                "Environment:Integration"};
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "questionnaire.feature"
#line hidden
        
        public QuestionnaireFeature(QuestionnaireFeature.FixtureData fixtureData, Synapxe_FhirWebApi_Relational_IntegrationTests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "Questionnaire", null, ProgrammingLanguage.CSharp, featureTags);
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
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "HeaderName",
                        "Value"});
            table8.AddRow(new string[] {
                        "X-Ihis-SourceApplication",
                        "testapp"});
#line 5
 testRunner.Given("a new HttpClient as httpClient", ((string)(null)), table8, "Given ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Reading a newly created Questionnaire returns the same Questionnaire")]
        [Xunit.TraitAttribute("FeatureTitle", "Questionnaire")]
        [Xunit.TraitAttribute("Description", "Reading a newly created Questionnaire returns the same Questionnaire")]
        public void ReadingANewlyCreatedQuestionnaireReturnsTheSameQuestionnaire()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Reading a newly created Questionnaire returns the same Questionnaire", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 9
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
#line 10
 testRunner.Given("a Resource is created from Samples/form-general-simple-version.json as createdRes" +
                        "ource", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 11
 testRunner.When("getting Questionnaire/{createdResource.Id} as readResource", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                            "Path",
                            "Value"});
                table9.AddRow(new string[] {
                            "statusCode",
                            "201"});
#line 12
 testRunner.Then("createdResource is a Fhir Questionnaire with data", ((string)(null)), table9, "Then ");
#line hidden
#line 15
 testRunner.And("createdResource and readResource are exactly the same", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Reading a newly created general form Questionnaire returns the same Questionnaire" +
            "")]
        [Xunit.TraitAttribute("FeatureTitle", "Questionnaire")]
        [Xunit.TraitAttribute("Description", "Reading a newly created general form Questionnaire returns the same Questionnaire" +
            "")]
        public void ReadingANewlyCreatedGeneralFormQuestionnaireReturnsTheSameQuestionnaire()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Reading a newly created general form Questionnaire returns the same Questionnaire" +
                    "", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 17
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
#line 18
 testRunner.Given("a Resource is created from Samples/Form-General.R5.json as createdResource", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 19
 testRunner.When("getting Questionnaire/{createdResource.Id} as readResource", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                            "Path",
                            "Value"});
                table10.AddRow(new string[] {
                            "statusCode",
                            "201"});
#line 20
 testRunner.Then("createdResource is a Fhir Questionnaire with data", ((string)(null)), table10, "Then ");
#line hidden
#line 23
 testRunner.And("createdResource and readResource are exactly the same", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Search a newly created general form Questionnaire by url, returns the bundle cont" +
            "ains a single Questionnaire")]
        [Xunit.TraitAttribute("FeatureTitle", "Questionnaire")]
        [Xunit.TraitAttribute("Description", "Search a newly created general form Questionnaire by url, returns the bundle cont" +
            "ains a single Questionnaire")]
        public void SearchANewlyCreatedGeneralFormQuestionnaireByUrlReturnsTheBundleContainsASingleQuestionnaire()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search a newly created general form Questionnaire by url, returns the bundle cont" +
                    "ains a single Questionnaire", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 25
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
                TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                            "Path",
                            "Value",
                            "FhirType"});
                table11.AddRow(new string[] {
                            "url",
                            "http://synapxe.sg/Questionnaire/{#uri}",
                            "uri"});
#line 26
 testRunner.Given("a Resource is created from Samples/form-general-simple-version.json with data as " +
                        "createdResource", ((string)(null)), table11, "Given ");
#line hidden
#line 29
 testRunner.When("getting Questionnaire?url=http://synapxe.sg/Questionnaire/{#uri}&_total=accurate&" +
                        "_sort=_lastUpdated as searchBundle", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 30
 testRunner.Then("searchBundle is a Fhir Bundle which contains createdResource", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                            "Path",
                            "Value",
                            "FhirType"});
                table12.AddRow(new string[] {
                            "statusCode",
                            "200",
                            ""});
                table12.AddRow(new string[] {
                            "total",
                            "1",
                            "int"});
#line 31
 testRunner.Then("searchBundle is a Fhir Bundle with data", ((string)(null)), table12, "Then ");
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
                QuestionnaireFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                QuestionnaireFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
