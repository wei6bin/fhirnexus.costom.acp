﻿using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Ihis.FhirEngine.Core.Handlers.Data;
using Ihis.FhirEngine.Data.Models;

namespace Synapxe.FhirWebApi.Relational.Data;

public class QuestionnaireResponseDataMapper : IFhirDataMapper<QuestionnaireResponseEntity, QuestionnaireResponse>
{
    public QuestionnaireResponse Map(QuestionnaireResponseEntity resource)
    {
        var questionnaireResponse = new QuestionnaireResponse()
        {
            Id = resource.Id,
            Authored = resource.Authored?.Value,
            // Subject is enforced to be mandatory
            Subject = new ResourceReference(resource.Subject!.Reference),
            // Questionnaire uri is enforced to be mandatory
            Questionnaire = resource.Questionnaire!.Value
        };

        if (resource.Author != null)
        {
            questionnaireResponse.Author = new ResourceReference(resource.Author.Reference);
        }

        // LinkId is mandatory as per FHIR spec
        var itemDictionary = resource.Item.ToDictionary(component => component.LinkId!);

        foreach (var item in resource.Item.Where(item => string.IsNullOrEmpty(item.ParentLinkId)))
        {
            questionnaireResponse.Item.Add(ConvertToItemComponent(item, itemDictionary));
        }

        return questionnaireResponse;
    }

    private QuestionnaireResponse.ItemComponent ConvertToItemComponent(
        QuestionnaireResponseEntity.ItemComponent entityItem,
        Dictionary<string, QuestionnaireResponseEntity.ItemComponent> itemDictionary)
    {
        var itemComponent = new QuestionnaireResponse.ItemComponent
        {
            LinkId = entityItem.LinkId,
            Text = entityItem.Text,
        };

        foreach (var answer in entityItem.Answer)
        {
            var value = answer.Value;
            var questionnaireResponseAnswer = new QuestionnaireResponse.AnswerComponent
            {
                Value = new FhirString(value.String),
            };
            itemComponent.Answer.Add(questionnaireResponseAnswer);
        }

        var subItems = itemDictionary.Values.Where(i => i.ParentLinkId == entityItem.LinkId).ToList();
        foreach (var subItem in subItems)
        {
            itemComponent.Item.Add(ConvertToItemComponent(subItem, itemDictionary));
        }

        return itemComponent;
    }

    public QuestionnaireResponseEntity ReverseMap(QuestionnaireResponse resource)
    {
        var entity = new QuestionnaireResponseEntity
        {
            Id = resource.Id,
            VersionId = int.TryParse(resource.Meta?.VersionId, out var versionId) ? versionId : 0,
            Status = resource.Status.GetLiteral(),
            LastUpdated = resource.Meta?.LastUpdated,
            Authored = new DateTimeEntity(resource.Authored),
            Subject = new ResourceReferenceEntity
            {
                Reference = resource.Subject!.Reference,
            },
            Questionnaire = new UriEntity { Value = resource.Questionnaire },
        };

        if (resource.Author is not null)
        {
            entity.Author = new ResourceReferenceEntity
            {
                Reference = resource.Author.Reference
            };
        }

        entity.Item = [];

        foreach (var item in resource.Item)
        {
            FlattenQuestionnaireResponseItemsRecursive(null, item, entity.Item);
        }

        return entity;
    }

    private void FlattenQuestionnaireResponseItemsRecursive(
        QuestionnaireResponse.ItemComponent? parentItem,
        QuestionnaireResponse.ItemComponent childItem,
        List<QuestionnaireResponseEntity.ItemComponent> result)
    {
        var childEntity = new QuestionnaireResponseEntity.ItemComponent
        {
            LinkId = childItem.LinkId,
            Text = childItem.Text,
            Answer = []
        };

        foreach (var answer in childItem.Answer)
        {
            DataEntity value = new();

            // TODO, append new data types
            switch (answer.Value.TypeName)
            {
                case "string":
                    value.TypeName = DataTypeName.String;
                    value.String = ((FhirString)answer.Value).Value;
                    break;
                case "Coding":
                    value.TypeName = DataTypeName.Coding;
                    value.Coding = new CodingEntity
                    {
                        Code = ((Coding)answer.Value).Code,
                        Display = ((Coding)answer.Value).Display,
                    };
                    break;
            }

            var questionnaireResponseAnswer = new QuestionnaireResponseEntity.AnswerComponent
            {
                Value = value
            };

            childEntity.Answer.Add(questionnaireResponseAnswer);
        }

        if (parentItem != null)
        {
            childEntity.ParentLinkId = parentItem.LinkId;
        }

        result.Add(childEntity);

        foreach (var subItem in childItem.Item)
        {
            FlattenQuestionnaireResponseItemsRecursive(childItem, subItem, result);
        }
    }
}